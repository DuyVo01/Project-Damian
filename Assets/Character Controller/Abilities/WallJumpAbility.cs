using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpAbility : IAbility
{
    private PlayerMovementInput playerInput;
    private PlayerManager playerManager;
    private PlayerData playerData;

    public WallJumpAbility(PlayerMovementInput playerInput, PlayerManager playerManager, PlayerData playerData)
    {
        this.playerInput = playerInput;
        this.playerManager = playerManager;
        this.playerData = playerData;
    }

    public void PerformAbility()
    {
        playerManager.SetGravity(playerData.gravityScale);
        playerManager.SetVelocity(Vector2.zero);

        Vector2 jumpForce = playerData.wallJumpForce;

        if (playerManager.GetPlayerSpeed().y < 0)
        {
            jumpForce.y -= playerManager.GetPlayerSpeed().y;
        }
        else if (playerManager.GetPlayerSpeed().y > 0)
        {
            jumpForce.y -= playerManager.GetPlayerSpeed().y;
        }
        jumpForce.x = playerData.isFacingRight ? -jumpForce.x : jumpForce.x;

        if (Mathf.Sign(jumpForce.x) != Mathf.Sign(playerManager.GetPlayerSpeed().x))
        {
            jumpForce.x -= playerManager.GetPlayerSpeed().x;
        }

        playerManager.AddForceToPlayer(jumpForce, 1);

        playerInput.Jump.IsActive = false;
        playerInput.JumpHold.IsActive = false;
    }
}
