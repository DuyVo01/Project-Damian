using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Mathematics;
using UnityEngine;

public class MovingState : GroundState
{
    public delegate void AnimationEventHandler(bool trueOrFalse);
    public static event AnimationEventHandler OnAnimationEvent;

    private bool exit;
    public MovingState(SharedStateDependency dependency) : base(dependency)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("moving State Entered");
        if (OnAnimationEvent != null)
        {
            OnAnimationEvent(true);
        }
        exit = false;
    }

    public override void Exit()
    {
        base.Exit();
        if (OnAnimationEvent != null)
        {
            OnAnimationEvent(false);
        }
        exit = true;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        MovementHandler();
        FrictionOnStop();
        if (movementInput.x != 0)
        {
            dependency.PlayerManager.LowerEdgeCheck.PerformActionCheck();
        }
    }

    public void MovementHandler()
    {
        float acceleration = dependency.PlayerData.speedAccelAmount;
        float deceleration = dependency.PlayerData.speedDecelAmount;

        float targetSpeed = movementInput.x * dependency.PlayerData.maxRunSpeed;
        targetSpeed = Mathf.Lerp(dependency.PlayerManager.GetPlayerSpeed().x, targetSpeed, 1);

        float speedDifference = targetSpeed - dependency.PlayerManager.GetPlayerSpeed().x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movementForce = speedDifference * accelRate;

        dependency.PlayerManager.AddForceToPlayer(Vector2.right * movementForce, 0);
    }

    public void FrictionOnStop()
    {
        if (MathF.Abs(movementInput.x) < 0.01f)
        {
            float amount = MathF.Min(MathF.Abs(dependency.PlayerManager.GetPlayerSpeed().x), dependency.PlayerData.frictionAmount);
            amount = amount * MathF.Sign(dependency.PlayerManager.GetPlayerSpeed().x);
            dependency.PlayerManager.AddForceToPlayer(Vector2.right * -amount, 0);
        }
    }

    public override void StateCheckTransition()
    {
        base.StateCheckTransition();
        if (exit == true)
        {
            return;
        }

        if (Mathf.Abs(dependency.PlayerManager.GetPlayerSpeed().x) <= 0.01f && movementInput.x == 0)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.IdleState);
        }
    }
}
