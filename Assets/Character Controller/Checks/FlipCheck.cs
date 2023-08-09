using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlipCheck : IActionCheck
{
    private PlayerMovementInput playerInput;
    private Transform playerTransform;
    private PlayerData playerData;
    public FlipCheck(PlayerMovementInput playerInput, Transform playerTransform, PlayerData playerData)
    {
        this.playerInput = playerInput;
        this.playerTransform = playerTransform;
        this.playerData = playerData;
    }

    
    public void PerformActionCheck()
    {
        CheckToFlip();
    }

    public void CheckToFlip()
    {
        if (playerInput.MovementVector.x < 0 && playerData.isFacingRight)
        {
            Flip();
        }
        else if (playerInput.MovementVector.x > 0 && !playerData.isFacingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        playerData.isFacingRight = !playerData.isFacingRight;
        playerTransform.localScale = new Vector3(-playerTransform.localScale.x, playerTransform.localScale.y, playerTransform.localScale.z);
    }
}
