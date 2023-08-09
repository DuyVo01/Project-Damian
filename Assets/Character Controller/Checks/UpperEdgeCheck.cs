using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class UpperEdgeCheck : IActionCheck
{
    private Transform farEdgeCheckPoint;
    private Transform nearEdgeCheckPoint;
    private Transform playerTransform;
    private PlayerManager playerManager;
    private LayerMask groundLayer;
    private PlayerData playerData;

    public UpperEdgeCheck(Transform farEdgeCheckPoint, Transform nearEdgeCheckPoint, LayerMask groundLayer, Transform playerTransform, PlayerManager playerManager, PlayerData playerData)
    {
        this.farEdgeCheckPoint = farEdgeCheckPoint;
        this.nearEdgeCheckPoint = nearEdgeCheckPoint;
        this.groundLayer = groundLayer;
        this.playerTransform = playerTransform;
        this.playerManager = playerManager;
        this.playerData = playerData;
    }

    public void PerformActionCheck()
    {

        Vector2 rayDir = nearEdgeCheckPoint.position - farEdgeCheckPoint.position;
        float rayLength = Mathf.Abs(Vector2.Distance(nearEdgeCheckPoint.position, farEdgeCheckPoint.position));
        RaycastHit2D hit = Physics2D.Raycast(farEdgeCheckPoint.position, rayDir.normalized, rayLength, groundLayer);

        if (hit.collider != null)
        {
            if (hit.distance > 0.0f)
            {
                float offsetHorizontal = rayLength - hit.distance;
                if (!playerData.isFacingRight)
                {
                    playerTransform.position += new Vector3(offsetHorizontal, 0, 0);

                }
                if (playerData.isFacingRight)
                {
                    playerTransform.position -= new Vector3(offsetHorizontal, 0, 0);

                }
            }
        }
    }

}
