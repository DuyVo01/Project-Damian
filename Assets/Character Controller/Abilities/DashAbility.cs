using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : IAbility
{
    private PlayerMovementInput playerInput;
    private PlayerManager playerManager;
    private PlayerData playerData;

    public DashAbility(PlayerMovementInput playerInput, PlayerManager playerManager, PlayerData playerData)
    {
        this.playerInput = playerInput;
        this.playerManager = playerManager;
        this.playerData = playerData;
    }
    public void PerformAbility()
    {
        float force = playerData.dashForce * playerInput.MovementVector.x;
        if(force == 0) 
        {
            force = playerData.dashForce;
        }
        Vector2 dashForce = force * Vector2.right;
        playerManager.AddForceToPlayer(dashForce, 1);
    }
}
