using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillState : AttackState
{
    public static event Action<string> OnSkillState;

    private bool isStageChangeDone;
    public SkillState(SharedStateDependency dependency, AttackHandler attackHandler) : base(dependency, attackHandler)
    {
    }

    public override void Enter()
    {
        base.Enter();
        attackMoveTime = 0;
        AttackAnimationEvents.OnSkillStageChange += SkillNextStage;

        isStageChangeDone = true;
        isAttackEnd = false;
        isAttackStart = true;

        ReadInput(dependency.PlayerMovementInput.CombinableInputList, dependency.PlayerAttackInput.CurrentAttackInput);
        currentAttack = attackHandler.GetAttack(executeLists.Dequeue());

        OnSkillState?.Invoke(currentAttack.AttackName);

        Debug.Log(currentAttack.AttackMoveDuration);
    }

    public override void Exit()
    {
        base.Exit();
        AttackAnimationEvents.OnSkillStageChange -= SkillNextStage;

        //attackHandler.ResetSkillData();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isAttackStart)
        {
            AttackMoving();
            isAttackStart = false;
            attackMoveTime = 0;
        }

        attackMoveTime += Time.deltaTime;
        if (attackMoveTime > currentAttack.AttackMoveDuration)
        {
            StopMoving();
        }
    }

    public override void StateCheckTransition()
    {
        base.StateCheckTransition();
        if (isAttackEnd && executeLists.Count > 0)
        {
            currentAttack = attackHandler.GetAttack(executeLists.Dequeue());
            if(currentAttack != null && currentAttack.AttackType == AttackType.normalAttack)
            {
                dependency.FiniteStateMachine.ChangeState(dependency.StateManager.AttackComboState);
            }
        }
    }

    public override void Update()
    {
        //base.Update();
        if (!isStageChangeDone)
        {
            currentAttack = attackHandler.SKillContinue();
            isStageChangeDone = true;
            isAttackStart = true;
        }
        StateCheckTransition();
    }

    private void SkillNextStage()
    {
        isStageChangeDone = false;
    }
}
