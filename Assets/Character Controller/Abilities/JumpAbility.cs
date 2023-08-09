using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : IAbility
{
    
    private PlayerMovementInput playerInput;
    private PlayerManager playerManager;
    private PlayerData playerData;

    public JumpAbility(PlayerMovementInput playerInput, PlayerManager playerManager, PlayerData playerData)
    {
        this.playerInput = playerInput;
        this.playerManager = playerManager;
        this.playerData = playerData;
    }

    public void PerformAbility()
    {
        playerManager.SetGravity(playerData.gravityScale);

        float force = playerData.jumpForce;
        
        if (playerManager.GetPlayerSpeed().y < 0)
        {
            force -= playerManager.GetPlayerSpeed().y;
        } 
        else if(playerManager.GetPlayerSpeed().y > 0)
        {
            force -= playerManager.GetPlayerSpeed().y;
        }

        Vector2 jumpForce = new Vector2((playerData.maxRunSpeed / 3) * playerInput.MovementVector.x, force);
        playerManager.AddForceToPlayer(jumpForce, 1);
        playerInput.Jump.IsActive = false;
        Debug.Log("Jump");
    }
}
