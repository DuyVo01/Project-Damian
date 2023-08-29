using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundState
{
    public delegate void AnimationEventHandler(bool trueOrFalse);
    public static event AnimationEventHandler OnAnimationEvent;

    public IdleState(SharedStateDependency dependency) : base(dependency)
    {

    }

    public override void Enter()
    {
        base.Enter();
        StopMoving();
        Debug.Log("Enter Idle State");
        if(OnAnimationEvent != null)
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

    public override void Update()
    {
        base.Update();
        if(movementInput.x != 0)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.MovingState);
        }
    }

    public override void FixedUpdate() 
    { 
        base.FixedUpdate();
    }

    private void StopMoving()
    {
        dependency.StateManager.PlayerManager.AddForceToPlayer(-dependency.PlayerManager.GetPlayerSpeed().x * Vector2.right, 1);
    }
}
