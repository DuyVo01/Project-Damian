using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBornState : IState
{
    protected SharedStateDependency dependency;
    protected Vector2 movementInput;
    private float maxSpeed;
    public AirBornState(SharedStateDependency dependency)
    {
        this.dependency = dependency;
    }

    public virtual void Enter()
    {
        maxSpeed = dependency.PlayerData.maxAirSpeed;
    }

    public virtual void Exit()
    {
    }

    public virtual void FixedUpdate()
    {
        AirMovementHandler();
        if (dependency.PlayerData.doubleJump && dependency.PlayerMovementInput.Jump.IsActive)
        {
            PerformDoubleJump();
        }
    }

    public virtual void Update()
    {
        movementInput = dependency.PlayerMovementInput.MovementVector;
        StateCheckTransition();
    }

    public void AirMovementHandler()
    {
        float acceleration = dependency.PlayerData.speedAccelAmount;
        float deceleration = dependency.PlayerData.speedDecelAmount;

        float airAccelMult = dependency.PlayerData.airAccelerationMult;
        float airDecelMult = dependency.PlayerData.airDecelerationMult;

        float lerpValue = 0.5f;

        float targetSpeed = maxSpeed * movementInput.x;

        targetSpeed = Mathf.Lerp(dependency.PlayerManager.GetPlayerSpeed().x, targetSpeed, lerpValue);

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration * airAccelMult : deceleration * airDecelMult;

        float speedDifference = targetSpeed - dependency.PlayerManager.GetPlayerSpeed().x;
        float movementForce = speedDifference * accelRate;

        dependency.PlayerManager.AddForceToPlayer(Vector2.right * movementForce, 0);
        
    }

    public virtual void StateCheckTransition()
    {
        if (PlayerInfor.Instance.IsGrounded)
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.MovingState);
        }
        else if (dependency.PlayerManager.WallCheck.PerformCheck())
        {
            dependency.FiniteStateMachine.ChangeState(dependency.StateManager.WallSlideState);
        }
    }

    private void PerformDoubleJump()
    {
        dependency.PlayerManager.JumpAbility.PerformAbility();
        dependency.PlayerData.doubleJump = false;
    }
}
