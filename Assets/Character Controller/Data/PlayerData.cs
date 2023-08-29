using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "SOAssetsMenu/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Global")]
    public float gravityStrength;
    public float gravityScale;
    public bool isFacingRight = true;

    [Space(10)]
    [Header("Movement - Run")]
    public float speedAcceleration;
    public float speedDeceleration;
    [HideInInspector] public float speedAccelAmount;
    [HideInInspector] public float speedDecelAmount;
    public float maxRunSpeed;

    [Space(10)]
    [Header("Movement - friction")]
    public float frictionAmount;

    [Space(10)]
    [Header("Air - Jump")]
    public bool doubleJump;
    public float jumpHeight;
    public float jumpForce;
    public float jumpTimeToApex;
    public float coyoteTime;

    [Space(10)]
    [Header("Air - hanging")]
    [Range(0,1)]public float hangingGravityMult;
    public float hangingSpeedThreshold;
    public float hangingAccelerationMult;
    public float hangingMaxSpeedMult;

    [Space(10)]
    [Header("Air - Falling")]
    public float maxFallSpeed;
    public float fallGravityMult;

    [Space(10)]
    [Header("Air - movement")]
    public float maxAirSpeed;
    [Range(0,1)]public float airAccelerationMult;
    [Range(0,1)]public float airDecelerationMult;

    [Space(10)]
    [Header("Dash")]
    public bool canDash;
    public float dashForce;
    public float dashDuration;
    public float dashDistance;
    public float dashMomentumMult;
    [Range(0, 1)]public float dashGravityMult;

    [Space(10)]
    [Header("Wall")]
    public Vector2 wallJumpForce;
    public float wallAirLerpControl;
    public float wallLeavingTime;
    public float wallStateExitTime;
    public float maxSlideSpeed;
    public float slideAccelerate;

    [Header("Wall Alternative")]
    public float wallJumpHeight;
    public float wallJumpDuration;


    [Space(10)]
    [Header("Check")]
    public Vector2 groundCheckSize;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float wallCheckAdditionalLength;

    [Space(10)]
    [Header("Attack")]
    public float attackDuration;

    private void OnValidate()
    {
        gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
        gravityScale = gravityStrength / Physics2D.gravity.y;

        speedAccelAmount = (50 * speedAcceleration) / maxRunSpeed;
        speedDecelAmount = (50 * speedDeceleration) / maxRunSpeed;

        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

        speedAcceleration = Mathf.Clamp(speedAcceleration, 0.01f, maxRunSpeed);
        speedDeceleration = Mathf.Clamp(speedDeceleration, 0.01f, maxRunSpeed);

        dashForce = dashDistance / dashDuration;
    }
}
