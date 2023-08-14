using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackState : IState
{
    protected bool isAttackEnd;
    protected bool isAttackStart;
    protected float attackMoveTime;
    protected IAttack currentAttack;

    protected SharedStateDependency dependency;
    protected AttackHandler attackHandler;

    protected Queue<AttackEnum> executeLists = new Queue<AttackEnum>();

    public AttackState(SharedStateDependency dependency, AttackHandler attackHandler)
    {
        this.dependency = dependency;
        this.attackHandler = attackHandler;
    }
    
    public virtual void Enter()
    {
        AttackAnimationEvents.OnAttackEnd += AttackAnimationEnd;
        PlayerAttackInput.OnAttackInputPressed += AttackInputReceive;
    }

    public virtual void Exit()
    {
        AttackAnimationEvents.OnAttackEnd -= AttackAnimationEnd;
        PlayerAttackInput.OnAttackInputPressed -= AttackInputReceive;

        dependency.PlayerAttackInput.ResetAttackInput();
        attackHandler.ResetAttackData();
    }

    public virtual void FixedUpdate()
    {
        
    }

    public virtual void StateCheckTransition()
    {
        if (isAttackEnd && executeLists.Count == 0)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.IdleState);
        }
    }

    public virtual void Update()
    {
        StateCheckTransition();
    }

    protected void AttackAnimationEnd()
    {
        isAttackEnd = true;
    }

    protected void AttackInputReceive(AttackInputProperties attackInput)
    {
        ReadInput(dependency.PlayerMovementInput.CombinableInputList, attackInput);
    }

    protected void ReadInput(List<InputProperties> inputCombination, AttackInputProperties attackInput)
    {
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
            executeLists.Enqueue(attackCombination.Attack);
            inputCombination.Clear();
            dependency.PlayerAttackInput.DeactivateAttack(attackInput);
            break;
        }
        Debug.Log(executeLists.Peek());
    }

    protected void StopMoving()
    {
        dependency.PlayerManager.AddForceToPlayer(-dependency.PlayerManager.GetPlayerSpeed().x * Vector2.right, 1);
    }

    protected void AttackMoving()
    {
        if (currentAttack == null)
        {
            return;
        }

        Vector2 movingForce = currentAttack.AttackMoveForce;

        if (!dependency.PlayerData.isFacingRight)
        {
            movingForce.x = -movingForce.x;
        }

        dependency.PlayerManager.AddForceToPlayer(movingForce, 1);
    }
}
