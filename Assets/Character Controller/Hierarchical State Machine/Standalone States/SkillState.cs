using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillState : IState
{
    private bool isSkillContinue;
    private bool isSkillEnd;

    private SharedStateDependency dependency;
    private AttackHandler attackHandler;
    private PlayerAttackInput playerAttackInput;

    public delegate void StateEventHandler(string attackName);
    public static event StateEventHandler OnSkillStateEvent;
    public SkillState(SharedStateDependency dependency, AttackHandler attackHandler, PlayerAttackInput playerAttackInput)
    {
        this.dependency = dependency;
        this.attackHandler = attackHandler;
        this.playerAttackInput = playerAttackInput;
    }
    public void Enter()
    {
        Debug.Log("Enter Skill State");
        isSkillEnd = false;
        isSkillContinue = false;

        AttackAnimationEvents.OnSkillContinue += OnSkillContinue;
        AttackAnimationEvents.OnSkillEnd += OnSkillEnd;
    }

    public void Exit()
    {
        AttackAnimationEvents.OnSkillContinue -= OnSkillContinue;
        AttackAnimationEvents.OnSkillEnd -= OnSkillEnd;

        attackHandler.ResetSkillData();
    }

    public void FixedUpdate()
    {

    }

    public void StateCheckTransition()
    {
        if(!isSkillContinue && isSkillEnd)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.IdleState);
        }
    }

    public void Update()
    {
        if (isSkillContinue && !isSkillEnd)
        {
            attackHandler.SKillContinue();
            isSkillContinue = false;
        }
        StateCheckTransition();
    }

    private void OnSkillContinue()
    {
        isSkillContinue = true;
    }

    private void OnSkillEnd()
    {
        isSkillEnd = true;
    }
}
