using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class StepCheck : MonoBehaviour
{
    [SerializeField] private Transform upperCheckPoint;
    [SerializeField] private Transform lowerCheckPoint;
    [SerializeField] private LayerMask groundLayer;

    private PlayerMovementInput playerInput;
    private Rigidbody2D playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerMovementInput>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(playerRb.velocity.x) > 0.1f)
        {
            PerformActionCheck();
        }
    }

    private void FixedUpdate()
    {
        
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
                transform.Translate(new Vector3(0, rayLength - hit.distance + 0.2f));

            }
        }
    }
}
