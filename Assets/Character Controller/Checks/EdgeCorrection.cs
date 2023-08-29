using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class EdgeCorrection : MonoBehaviour
{
    public static bool ShouldCheck { get; set; }

    [SerializeField] private LayerMask layerToCheck;
    [SerializeField] private Transform edgeCheckPoint;
    [SerializeField] private float checkRadius;
    [SerializeField] private float amountToShrink;

    private BoxCollider2D playerCollider;

    private float originalColliderHeight;
    private float originalColliderWidth;

    private void OnEnable()
    {
        JumpAbility.OnJump += ShrinkColliderWidthSize;
    }

    private void OnDisable()
    {
        JumpAbility.OnJump -= ShrinkColliderWidthSize;
    }

    private void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();

        originalColliderHeight = playerCollider.size.y;
        originalColliderWidth = playerCollider.size.x;

        ShouldCheck = false;
    }

    private void FixedUpdate()
    {
        if (!ShouldCheck)
        {
            if(playerCollider.size != new Vector2(originalColliderWidth, originalColliderHeight))
            {
                playerCollider.size = new Vector2(originalColliderWidth, originalColliderHeight);
            }

            return;
        }

        EdgeDetection();
    }

    private void ShrinkColliderWidthSize()
    {
        playerCollider.size = new Vector2(originalColliderWidth - amountToShrink, originalColliderHeight);
    }

    private void EdgeDetection()
    {
        if(Physics2D.OverlapCircle(edgeCheckPoint.position, checkRadius, layerToCheck))
        {
            playerCollider.size = new Vector2(originalColliderWidth, originalColliderHeight);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(edgeCheckPoint.position, checkRadius);
    }
}
