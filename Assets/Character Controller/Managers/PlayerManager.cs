using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody2D playerRb { get; private set; }
    [SerializeField] private Transform groundCheckPoint;

    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerMovementInput playerInput;

    public WallCheck WallCheck { get; private set; }
    public FlipCheck FlipCheck { get; private set; }

    private Transform playerTransform;
    public bool isFacingRight { get; private set; }

    public JumpAbility JumpAbility { get; private set; }
    public WallJumpAbility WallJumpAbility { get; private set; }
    public DashAbility DashAbility { get; private set; }

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        playerInput = GetComponent<PlayerMovementInput>();

        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        SetGravity(playerData.gravityScale);
        playerData.isFacingRight = true;

        WallCheck = new WallCheck(playerData, this, playerData.groundLayer, playerInput);
        FlipCheck = new FlipCheck(playerInput, playerTransform, playerData);

        JumpAbility = new JumpAbility(playerInput, this, playerData);
        WallJumpAbility = new WallJumpAbility(playerInput, this, playerData);
        DashAbility = new DashAbility(playerInput, this, playerData);
        
    }

    private void Update()
    {
        FlipCheck.PerformActionCheck();
    }

    public Vector2 GetPlayerSpeed()
    {
        return playerRb.velocity;
    }

    public void AddForceToPlayer(Vector2 forceToAdd, int forceMode)
    {
        if(forceMode == 0)
        {
            playerRb.AddForce(forceToAdd, ForceMode2D.Force);
        } else if(forceMode == 1)
        {
            playerRb.AddForce(forceToAdd, ForceMode2D.Impulse);
        }
    }

    public void SetVelocity(Vector2 velocity)
    {
        playerRb.velocity = velocity;
    }

    public void SetGravity(float gravityScale)
    {
        playerRb.gravityScale = gravityScale;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector2 rayWallDir = Vector2.right;
        if(playerData.isFacingRight == false)
        {
            rayWallDir = new Vector2(-1, 0);
        }
        Gizmos.DrawRay(transform.position, rayWallDir * (GetComponent<Collider2D>().bounds.size.x/2 + playerData.wallCheckAdditionalLength));
        
    }
}
