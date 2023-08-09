using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AttackState : IState
{
    private bool isAttackEnd;
    private IAttack currentAttack;

    private SharedStateDependency dependency;
    private AttackHandler attackData;

    private Queue<AttackEnum> executeLists = new Queue<AttackEnum>();

    public delegate void StateEventHandler(string attackName);
    public static event StateEventHandler OnAttackStateEvent;
    public AttackState(SharedStateDependency dependency, AttackHandler attackData)
    {
        this.dependency = dependency;
        this.attackData = attackData;
    }

    public void Enter()
    {
        StopMoving();

        AttackAnimationEvents.OnAttackEnd += AttackAnimationEnd;
        PlayerAttackInput.OnAttackInputPressed += CombinationChange;

        ReadInput(dependency.PlayerMovementInput.CombinableInputList, dependency.PlayerAttackInput.CurrentAttackInput);

        if (executeLists.Count > 0)
        {
            currentAttack = attackData.GetAttack(executeLists.Dequeue());

            if (currentAttack != null)
            {
                OnAttackStateEvent?.Invoke(currentAttack.AttackName);
                isAttackEnd = false;
            }
            else
            {
                isAttackEnd = true;
            }
        }
    }

    public void Exit()
    {
        AttackAnimationEvents.OnAttackEnd -= AttackAnimationEnd;
        PlayerAttackInput.OnAttackInputPressed -= CombinationChange;

        //dependency.PlayerInput.DeactivateAttackInput();
        dependency.PlayerAttackInput.ResetAttackInput();
        attackData.ResetAttackData();
    }

    public void FixedUpdate()
    {

    }

    public void StateCheckTransition()
    {
        if (isAttackEnd && executeLists.Count == 0)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.IdleState);
        }
        if (currentAttack != null && currentAttack.AttackType == AttackType.skillAttack)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.SkillState);
        }
    }

    public void Update()
    {
        StateCheckTransition();
        ExecuteAttack();
    }

    private void ExecuteAttack()
    {
        if (isAttackEnd && executeLists.Count > 0)
        {
            currentAttack = attackData.GetAttack(executeLists.Dequeue());
            
            if (currentAttack != null)
            {
                Debug.Log(currentAttack.AttackName);
                OnAttackStateEvent(currentAttack.AttackName);
                isAttackEnd = false;
            }
        }
    }

    private void AttackAnimationEnd()
    {
        isAttackEnd = true;
    }

    private void CombinationChange(AttackInputProperties attackInput)
    {
        ReadInput(dependency.PlayerMovementInput.CombinableInputList, attackInput);
    }

    private void StopMoving()
    {
        dependency.PlayerManager.AddForceToPlayer(-dependency.PlayerManager.GetPlayerSpeed().x * Vector2.right, 1);
    }

    private void ReadInput(List<InputProperties> inputCombination, AttackInputProperties attackInput)
    {
        AttackEnum inputResult = AttackEnum.noAttack;

        AttackCombination attack = new AttackCombination();

        attack.InputModifier01 = InputEnum.noInput;
        attack.InputModifier02 = InputEnum.noInput;
        attack.InputAttack = attackInput.InputType;

        inputCombination.Reverse();

        for (int i = 0; i < inputCombination.Count; i++)
        {
            if (!inputCombination[i].isInCombine)
            {
                continue;
            }

            else if (i == 0)
            {
                attack.InputModifier02 = inputCombination[i].InputType;
                Debug.Log(attack.InputModifier02);
            }
            else if (i == 1)
            {
                attack.InputModifier01 = inputCombination[i].InputType;
                Debug.Log(attack.InputModifier01);
            }
        }

        foreach (AttackCombination attackCombination in dependency.PlayerAttackInput.CombinationSet.ToList())
        {
            if (attack.InputModifier01 != InputEnum.noInput)
            {
                if (attackCombination.InputModifier01 != attack.InputModifier01)
                {
                    continue;
                }
            }

            if (attack.InputModifier02 != InputEnum.noInput)
            {
                if (attackCombination.InputModifier02 != attack.InputModifier02)
                {
                    continue;
                }
            }

            if (attackCombination.InputAttack != attack.InputAttack)
            {
                continue;
            }

            inputResult = attackCombination.Attack;
            executeLists.Enqueue(inputResult);
            inputCombination.Clear();
            dependency.PlayerAttackInput.DeactivateAttack(attackInput);
            break;
        }
        Debug.Log(executeLists.Peek());
    }
}