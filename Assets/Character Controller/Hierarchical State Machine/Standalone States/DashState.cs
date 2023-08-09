using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : IState
{
    private float enterStartTime;
    private SharedStateDependency dependency;
    public DashState(SharedStateDependency dependency)
    {
        this.dependency = dependency;
    }

    public void Enter()
    {
        Debug.Log("Enter Dash State");
        enterStartTime = Time.time;
        dependency.PlayerData.canDash = false;
        dependency.PlayerManager.SetGravity(dependency.PlayerData.dashGravityMult * dependency.PlayerData.gravityScale);
        PerformDash();
    }

    public void Exit()
    {
        StopDash();
        dependency.PlayerData.canDash = true;
        dependency.PlayerManager.SetGravity(dependency.PlayerData.gravityScale);
    }

    public void FixedUpdate()
    {
    }

    public void StateCheckTransition()
    {
        if (dependency.PlayerManager.GroundCheck.PerformCheck())
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.MovingState);
        } 
        else if (dependency.PlayerManager.GetPlayerSpeed().y < 0)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.FallingState);
        }
        else if (dependency.PlayerManager.GetPlayerSpeed().y > 0)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.JumpState);
        }
    }

    public void Update()
    {
        dependency.PlayerManager.LowerEdgeCheck.PerformActionCheck();
        if (Time.time - enterStartTime > dependency.PlayerData.dashDuration)
        {
            StateCheckTransition();
        }
    }

    private void PerformDash()
    {
        float force = dependency.PlayerData.dashForce * dependency.PlayerMovementInput.MovementVector.x;
        if (force == 0)
        {
            force = dependency.PlayerData.dashForce;
        }

        if (dependency.PlayerManager.GetPlayerSpeed().x < 0)
        {
            force -= dependency.PlayerManager.GetPlayerSpeed().x;
        }
        else if (dependency.PlayerManager.GetPlayerSpeed().x > 0)
        {
            force -= dependency.PlayerManager.GetPlayerSpeed().x;
        }

        Vector2 dashForce = force * Vector2.right;
        dependency.PlayerManager.AddForceToPlayer(dashForce, 1);
        dependency.PlayerMovementInput.Dash.IsActive = false;
    }

    private void StopDash()
    {
        float force = dependency.PlayerManager.GetPlayerSpeed().x;
        Vector2 dashForce = force * Vector2.right;
        dependency.PlayerManager.AddForceToPlayer(-dashForce * dependency.PlayerData.dashMomentumMult, 1);
    }
}
