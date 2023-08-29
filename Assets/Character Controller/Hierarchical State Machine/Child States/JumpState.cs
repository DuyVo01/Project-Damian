using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AirBornState
{
    public delegate void JumpStateEvent<T>(T EventArgs);

    public static event JumpStateEvent<bool> OnAnimationEvent;

    public JumpState(SharedStateDependency dependency) : base(dependency)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Enter Jump State");

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

        EdgeCorrection.ShouldCheck = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        
        JumpCut();
    }

    public override void Update()
    {
        base.Update();
        //HangingState();

        if (dependency.PlayerManager.GetPlayerSpeed().y < 0)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.FallingState);
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
