using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerEdgeCheck : IActionCheck
{
    private Transform lowerCheckPoint;
    private Transform upperCheckPoint;
    private Transform playerTransform;
    private LayerMask groundLayer;
    public LowerEdgeCheck(Transform lowerCheckPoint, Transform upperCheckPoint, LayerMask groundLayer, Transform playerTransform)
    {
        this.lowerCheckPoint = lowerCheckPoint;
        this.upperCheckPoint = upperCheckPoint;
        this.groundLayer = groundLayer;
        this.playerTransform = playerTransform;
    }

    public void PerformActionCheck()
    {
        Vector2 rayDir = lowerCheckPoint.position - upperCheckPoint.position;
        float rayLength = Mathf.Abs(Vector2.Distance(lowerCheckPoint.position, upperCheckPoint.position));
        RaycastHit2D hit = Physics2D.Raycast(upperCheckPoint.position, rayDir.normalized, rayLength, groundLayer);

        if (hit.collider != null)
        {
            if (hit.distance > 0.0f)
            {
                playerTransform.Translate(new Vector3(0, rayLength - hit.distance + 0.2f));

            }
        }
    }
}
