using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public static bool ShouldCheck { get; set; }

    [SerializeField] private Transform checkPoint;
    [SerializeField] private Vector2 checkSize;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        ShouldCheck = true;
    }
    private void FixedUpdate()
    {
        if (!ShouldCheck)
        {
            PlayerInfor.Instance.IsGrounded = false;
            return;
        }

        PerformCheck();
    }

    public void PerformCheck()
    {
        if(Physics2D.OverlapBox(checkPoint.position, checkSize, 0, groundLayer))
        {
            PlayerInfor.Instance.IsGrounded = true;
        }
        else
        {
            PlayerInfor.Instance.IsGrounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(checkPoint.position, checkSize);
    }
}
