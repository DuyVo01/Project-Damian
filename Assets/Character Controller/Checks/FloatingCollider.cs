using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCollider : MonoBehaviour
{
    private Collider2D playerCollider;
    private Rigidbody2D playerRb;

    [SerializeField] float rayCastDistance;
    [SerializeField] float offsetFloatingDistance;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] float positionPreservingForce;

    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!PlayerInfor.Instance.IsGrounded)
        {
            return;
        }

        Vector2 colliderCenterInWorldSpace = playerCollider.bounds.center;

        Ray downwardRayFromColliderCenter = new Ray(colliderCenterInWorldSpace, Vector2.down);

        RaycastHit2D hit = Physics2D.Raycast(downwardRayFromColliderCenter.origin, downwardRayFromColliderCenter.direction, rayCastDistance, groundLayerMask);

        if (hit.collider == null)
        {
            return;
        }

        float distanceToFloatingPoint = transform.InverseTransformPoint(playerCollider.bounds.center).y + offsetFloatingDistance * transform.localScale.y - hit.distance;

        if (distanceToFloatingPoint == 0)
        {
            return;
        }

        float amountToLift = distanceToFloatingPoint * positionPreservingForce - playerRb.velocity.y;

        Vector2 LiftForce = new Vector2(0, amountToLift);

        playerRb.AddForce(LiftForce, ForceMode2D.Impulse);
    }
}
