using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class WallJumpState : IState
{
    private float enterStateTime;
    private SharedStateDependency dependency;
    public WallJumpState(SharedStateDependency dependency)
    {
        this.dependency = dependency;
    }

    public void Enter()
    {
        enterStateTime = Time.time;
        Debug.Log("Enter Wall Jump");
        dependency.PlayerManager.WallJumpAbility.PerformAbility();
        dependency.PlayerData.doubleJump = false;
    }

    public void Exit()
    {
       
    }

    public void FixedUpdate()
    {
        
    }

    public void StateCheckTransition()
    {
        
        if (ExitWallJump())
        {
            if (dependency.PlayerManager.WallCheck.PerformCheck())
            {
                dependency.FiniteStateMachine.ChangeState(dependency.StateManager.WallSlideState);
            }
            else
            {
                dependency.FiniteStateMachine.ChangeState(dependency.StateManager.JumpState);
            }
        }
    }

    public void Update()
    {
        StateCheckTransition();
    }

    private bool ExitWallJump()
    {
        if (Time.time - enterStateTime > dependency.PlayerData.wallStateExitTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
