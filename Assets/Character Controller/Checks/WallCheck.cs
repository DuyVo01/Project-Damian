using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : IBooleancheck
{
    private PlayerData playerData;
    private PlayerManager playerManager;
    private PlayerMovementInput playerInput;
    private LayerMask wallLayer;

    public WallCheck(PlayerData playerData, PlayerManager playerManager, LayerMask wallLayer, PlayerMovementInput playerInput)
    {
        this.playerData = playerData;
        this.playerManager = playerManager;
        this.wallLayer = wallLayer;
        this.playerInput = playerInput;
    }
    public bool PerformCheck()
    {
        Collider2D collider2D = playerManager.GetComponent<Collider2D>();
        if (collider2D != null)
        {
            Vector2 rayDir = Vector2.right;
            float rayLength = collider2D.bounds.size.x / 2 + playerData.wallCheckAdditionalLength;
            if (playerInput.MovementVector.x < 0)
            {
                rayDir = new Vector2(-1, 0);
            } 
            else if (playerInput.MovementVector.x == 0)
            {
                rayDir = Vector2.zero;
            }

            if (Physics2D.Raycast(playerManager.transform.position, rayDir, rayLength, wallLayer))
            {
                return true;
            }
        }
        return false;
    }
}
