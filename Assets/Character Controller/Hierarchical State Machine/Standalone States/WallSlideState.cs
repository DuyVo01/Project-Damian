using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallSlideState : IState
{
    private float exitStateTime;

    public delegate void AnimationEventHandler(bool trueOrFalse);
    public static event AnimationEventHandler OnAnimationEvent;

    SharedStateDependency dependency;
    public WallSlideState(SharedStateDependency dependency)
    {
        this.dependency = dependency;
    }

    public void Enter()
    {
        exitStateTime = 0;
        if (OnAnimationEvent != null)
        {
            OnAnimationEvent(true);
        }
    }

    public void Exit()
    {
        if (OnAnimationEvent != null)
        {
            OnAnimationEvent(false);
        }
    }

    public void FixedUpdate()
    {
        Sliding();
    }

    public void StateCheckTransition()
    {
        if (dependency.PlayerManager.WallCheck.PerformCheck() && dependency.PlayerMovementInput.Jump.IsActive)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.WallJumpState);
        }
        else if (!dependency.PlayerManager.WallCheck.PerformCheck())
        {
            ExitWallState();
        }
    }

    public void Update()
    {
        StateCheckTransition();
    }

    private void Sliding()
    {
        Vector2 velocityClamp = new Vector2(dependency.PlayerManager.GetPlayerSpeed().x, Mathf.Max(dependency.PlayerManager.GetPlayerSpeed().y, dependency.PlayerManager.GetPlayerSpeed().y, -dependency.PlayerData.maxSlideSpeed));
        dependency.PlayerManager.SetVelocity(velocityClamp);
    }

    private void ExitWallState()
    {
        exitStateTime += Time.deltaTime;
        if (exitStateTime < dependency.PlayerData.wallLeavingTime)
        {
            if(dependency.PlayerMovementInput.Jump.IsActive)
            {
                dependency.FiniteStateMachine.ChangeState(dependency.StateManager.WallBounceState);
            }
        }
        else
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.FallingState);
            dependency.PlayerData.doubleJump = true;
        }
    }
}
