using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : IBooleancheck
{
    private Transform checkPoint;
    private Vector2 checkSize;
    private LayerMask groundLayer;

    public GroundCheck(Transform checkPoint, Vector2 checkSize, LayerMask groundLayer) 
    {
        this.checkPoint = checkPoint;
        this.checkSize = checkSize;
        this.groundLayer = groundLayer;
    }

    public bool PerformCheck()
    {
        return Physics2D.OverlapBox(checkPoint.position, checkSize, 0, groundLayer);
    }
}
