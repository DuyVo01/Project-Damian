using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : AirBornState
{
    public delegate void AnimationEventHandler(bool trueOrFalse);
    public static event AnimationEventHandler OnAnimationEvent;
    public FallingState(SharedStateDependency dependency) : base(dependency)
    {
    }

    public override void Enter()
    {
        base.Enter();
        GroundCheck.ShouldCheck = true;
        EdgeCorrection.ShouldCheck = false;
        Debug.Log("Enter Falling State");

        dependency.PlayerManager.SetGravity(dependency.PlayerData.gravityScale);

        if (OnAnimationEvent != null)
        {
            OnAnimationEvent(true);
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (OnAnimationEvent != null)
        {
            OnAnimationEvent(false);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        FasterFalling();
        FallingSpeedCap();
    }

    public override void Update()
    {
        base.Update();

        if (dependency.PlayerManager.GetPlayerSpeed().y > 0 && !dependency.PlayerManager.WallCheck.PerformCheck())
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.JumpState);
        }
    }

    private void FallingSpeedCap()
    {
        Vector2 fallingSpeedCap = new Vector2(dependency.PlayerManager.GetPlayerSpeed().x, Mathf.Max(dependency.PlayerManager.GetPlayerSpeed().y, -dependency.PlayerData.maxFallSpeed));
        dependency.PlayerManager.SetVelocity(fallingSpeedCap);
    }

    private void FasterFalling()
    {
        dependency.PlayerManager.playerRb.AddForce(Vector2.down * dependency.PlayerData.maxFallSpeed * 2, ForceMode2D.Force);
    }
}
