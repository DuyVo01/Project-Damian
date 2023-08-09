using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : IState
{
    protected Vector2 movementInput;
    private float coyoteStartTime;
    private bool performJump;

    protected SharedStateDependency dependency;
    public GroundState(SharedStateDependency dependency)
    {
       this.dependency = dependency;
    }

    public virtual void Enter()
    {
        dependency.PlayerManager.SetGravity(dependency.PlayerData.gravityScale);
        performJump = false;

        dependency.PlayerData.canDash = true;
        coyoteStartTime = 0;
        dependency.PlayerData.doubleJump = true;
    }
    public virtual void Exit()
    {
        
    }

    public virtual void Update()
    {
        movementInput = dependency.PlayerMovementInput.MovementVector;
        StateCheckTransition();
    }

    public virtual void FixedUpdate()
    {
        if (dependency.PlayerMovementInput.Jump.IsActive && !performJump && !dependency.PlayerManager.WallCheck.PerformCheck())
        {
            PerformJump();
        }
    }

    public virtual void StateCheckTransition()
    {   
        if (dependency.PlayerAttackInput.AttackButtonActive)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.AttackState);
        }
        else if (dependency.PlayerManager.GetPlayerSpeed().y > 0 && !dependency.PlayerManager.GroundCheck.PerformCheck() && !dependency.PlayerManager.WallCheck.PerformCheck())
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.JumpState);
        }
        else if (dependency.PlayerMovementInput.Jump.IsActive && !performJump && dependency.StateManager.PlayerManager.WallCheck.PerformCheck())
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.WallJumpState);
        }
        else if (dependency.PlayerManager.GetPlayerSpeed().y < 0 && !dependency.PlayerManager.GroundCheck.PerformCheck())
        {
            if (coyoteStartTime == 0)
            {
                coyoteStartTime = Time.time;
            }

            if (CoyoteTimeOver())
            {
                dependency.FiniteStateMachine.ChangeState(dependency.StateManager.FallingState);
            }
            else if (dependency.PlayerMovementInput.Jump.IsActive)
            {
                PerformJump();
            }
        }
        else if (dependency.StateManager.playerData.canDash && dependency.PlayerMovementInput.Dash.IsActive)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.DashState);
        } 
    }

    bool CoyoteTimeOver()
    {
        if (Time.time - coyoteStartTime >= dependency.PlayerData.coyoteTime)
        {
            return true;
        }
        return false;
    }

    void PerformJump()
    {
        dependency.PlayerManager.JumpAbility.PerformAbility();
        performJump = true;
    }

}

