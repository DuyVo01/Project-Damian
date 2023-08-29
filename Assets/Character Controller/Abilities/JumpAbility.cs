using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : IAbility
{
    private PlayerMovementInput playerInput;
    private PlayerManager playerManager;
    private PlayerData playerData;

    public static event Action OnJump;

    public JumpAbility(PlayerMovementInput playerInput, PlayerManager playerManager, PlayerData playerData)
    {
        this.playerInput = playerInput;
        this.playerManager = playerManager;
        this.playerData = playerData;
    }

    public void PerformAbility()
    {
        OnJump?.Invoke();

        GroundCheck.ShouldCheck = false;

        EdgeCorrection.ShouldCheck = true;

        playerManager.SetGravity(playerData.gravityScale);

        float force = playerData.jumpForce;

        playerManager.playerRb.velocity = Vector2.zero;

        Vector2 jumpForce = new Vector2(playerData.maxAirSpeed * playerInput.MovementVector.x, force);
        playerManager.playerRb.AddForce(jumpForce, ForceMode2D.Impulse);
        playerInput.Jump.IsActive = false;
    }
}
