using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AttackComboState : AttackState
{
    public delegate void StateEventHandler(string attackName);
    public static event StateEventHandler OnAttackStateEvent;

    public AttackComboState(SharedStateDependency dependency, AttackHandler attackData) : base(dependency, attackData)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        StopMoving();

        attackMoveTime = 0;

        ReadInput(dependency.PlayerMovementInput.CombinableInputList, dependency.PlayerAttackInput.CurrentAttackInput);

        if (executeLists.Count > 0)
        {
            currentAttack = attackHandler.GetAttack(executeLists.Dequeue());
        }

        if (currentAttack != null)
        {
            if (currentAttack.AttackType == AttackType.skillAttack)
            {
                return;
            }

            OnAttackStateEvent?.Invoke(currentAttack.AttackName);
            isAttackEnd = false;
            isAttackStart = true;
        }
        else
        {
            isAttackEnd = true;
        }

        Debug.Log(currentAttack);
    }

    public override void Exit()
    {
        base.Exit();
        attackHandler.ResetAttackData();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(currentAttack == null)
        {
            return;
        }

        if (isAttackStart)
        {
            AttackMoving();
            isAttackStart = false;
            attackMoveTime = 0;
        }

        attackMoveTime += Time.deltaTime;
        if(attackMoveTime > currentAttack.AttackMoveDuration)
        {
            StopMoving();
        }
    }

    public override void StateCheckTransition()
    {
        base.StateCheckTransition();
        if (currentAttack != null && currentAttack.AttackType == AttackType.skillAttack)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.SkillState);
        }
    }

    public override void Update()
    {
        //base.Update();
        ExecuteAttack();
        StateCheckTransition();
    }

    private void ExecuteAttack()
    {
        if (isAttackEnd && executeLists.Count > 0)
        {
            currentAttack = attackHandler.GetAttack(executeLists.Dequeue());
            if (currentAttack != null && isAttackEnd)
            {
                Debug.Log(currentAttack.AttackName);
                OnAttackStateEvent(currentAttack.AttackName);
                isAttackEnd = false;
                isAttackStart = true;
            }
        }
    }
}