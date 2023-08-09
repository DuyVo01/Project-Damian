using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AirBornState
{
    private bool hasSetHangingGravity;

    public delegate void AnimationEventHandler(bool trueOrFalse);
    public static event AnimationEventHandler OnAnimationEvent;
    public JumpState(SharedStateDependency dependency) : base(dependency)
    {
    }

    public override void Enter()
    {
        base.Enter();
        hasSetHangingGravity = false;
        Debug.Log("Enter Jump State");
       
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
        
        JumpCut();
    }

    public override void Update()
    {
        base.Update();
        HangingState();
        dependency.PlayerManager.UpperEdgeCheck.PerformActionCheck();
        if (dependency.PlayerManager.GetPlayerSpeed().y < 0)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.FallingState);
        }
        
    }

    private void HangingState()
    {
        if (Mathf.Abs(dependency.PlayerManager.GetPlayerSpeed().y) < dependency.PlayerData.hangingSpeedThreshold && !hasSetHangingGravity)
        {
            dependency.PlayerManager.SetGravity(dependency.PlayerData.gravityScale * dependency.PlayerData.hangingGravityMult);
            hasSetHangingGravity = true;
        }
    }

    private void JumpCut()
    {
        if (!dependency.PlayerMovementInput.JumpHold.IsActive)
        {
            float currentVerticalSpeed = dependency.PlayerManager.GetPlayerSpeed().y;
            Vector2 negativeForce = Vector2.down * currentVerticalSpeed;
            dependency.PlayerManager.AddForceToPlayer(negativeForce * 2, 0);
        }
    }
}
