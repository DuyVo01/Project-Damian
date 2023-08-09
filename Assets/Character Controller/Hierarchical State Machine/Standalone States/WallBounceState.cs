using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounceState : IState
{
    private bool isBounce;
    SharedStateDependency dependency;
    public WallBounceState(SharedStateDependency dependency)
    {
        this.dependency = dependency;
    }

    public void Enter()
    {
        isBounce = false;
        BounceOff();
    }

    public void Exit()
    {
        dependency.PlayerData.doubleJump = true;
    }

    public void FixedUpdate()
    {
    }

    public void StateCheckTransition()
    {
        if(isBounce)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.JumpState);
        }
    }

    public void Update()
    {
        StateCheckTransition();
    }

    private void BounceOff()
    {
        float horizontalForce = dependency.PlayerData.maxAirSpeed * 4f * dependency.PlayerMovementInput.MovementVector.x;
        float verticalForce = dependency.PlayerData.jumpForce;

        if (dependency.PlayerManager.GetPlayerSpeed().y < 0)
        {
            verticalForce -= dependency.PlayerManager.GetPlayerSpeed().y;
        }
        else if (dependency.PlayerManager.GetPlayerSpeed().y > 0)
        {
            verticalForce -= dependency.PlayerManager.GetPlayerSpeed().y;
        }

        if (Mathf.Sign(horizontalForce) != Mathf.Sign(dependency.PlayerManager.GetPlayerSpeed().x))
        {
            horizontalForce -= dependency.PlayerManager.GetPlayerSpeed().x;
        }

        Vector2 forceToAdd = new Vector2(horizontalForce, verticalForce);
        dependency.PlayerManager.AddForceToPlayer(forceToAdd, 1);
        isBounce = true;

        dependency.PlayerMovementInput.Jump.IsActive = false;
        dependency.PlayerMovementInput.JumpHold.IsActive = false;
    }
}
