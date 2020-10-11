using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private LayerMask platformMask;
    [SerializeField]
    private LayerMask wallMask;

    private string prevWall = "";
    public Rigidbody2D playerRB;
    private BoxCollider2D playerBC;

    private int jumpCount;
    private int dashCount;
    private int maxJumpCount;
    private int maxDashCount;
    [SerializeField]
    private float scale = 2;
    private bool inDash = false;

    private void Awake()
    {
        playerBC = transform.GetComponent<BoxCollider2D>();
        playerRB = transform.GetComponent<Rigidbody2D>();
        GameController.Instance.playerRB = playerRB;
        GameController.Instance.playerBC = playerBC;
        GameController.Instance.platformMask = platformMask;
        animator = GetComponent<Animator>();
        maxJumpCount = 1;
        maxDashCount = 1;
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(GameController.Instance.playerBC.bounds.center, GameController.Instance.playerBC.bounds.size, 0f, Vector2.down, 1f, platformMask);
        return hit.collider != null;
    }

    public void JumpController()
    {
        float jumpVelocity = 35f;
        if (!Input.GetKey(KeyCode.S))
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                if (IsGrounded())
                {
                    GameController.Instance.playerRB.velocity = Vector2.up * jumpVelocity;
                    animator.SetInteger("AnimState", 3);
                }
                else
                if (jumpCount < maxJumpCount && !IsWallJump())
                {
                    GameController.Instance.playerRB.velocity = new Vector2(GameController.Instance.playerRB.velocity.x, jumpVelocity);
                    jumpCount++;
                }
            }

    }

    public bool IsWallJump()
    {
        float jumpVelocity = 35f;

        RaycastHit2D hit = Physics2D.BoxCast(GameController.Instance.playerBC.bounds.center, GameController.Instance.playerBC.bounds.size, 0f, Vector2.right, 0.5f, wallMask);
        if (hit.collider != null)
            if (prevWall != hit.collider.gameObject.name)
            {
                GameController.Instance.playerRB.velocity = Vector2.up * jumpVelocity + Vector2.left * jumpVelocity / 2;
                prevWall = hit.collider.gameObject.name;
            }
            else
            {
                return false;
            }
        return hit.collider != null;
    }

    public void DashController()
    {
        float dashVelocity = 45f;
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
        {
            if (IsGrounded())
            {
                StartCoroutine(StayInDash());
                GameController.Instance.playerRB.velocity = new Vector2(-transform.localScale.x / scale, 0) * dashVelocity;
            }
            else
            if (dashCount < maxDashCount)
            {
                StartCoroutine(StayInDash());
                GameController.Instance.playerRB.velocity = new Vector2(-transform.localScale.x / scale, 0) * dashVelocity;
                dashCount++;
            }
        }
    }

    public void Flip()
    {
        Vector2 charScale = transform.localScale;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            charScale.x = -scale;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            charScale.x = scale;
        }
        transform.localScale = charScale;
    }

    public void MovementController()
    {
        float moveSpeed = 20f;
        if (!inDash)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                GameController.Instance.playerRB.velocity = new Vector2(-moveSpeed, playerRB.velocity.y);
                animator.SetInteger("AnimState", 2);
            }
            else
            {
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    GameController.Instance.playerRB.velocity = new Vector2(+moveSpeed, playerRB.velocity.y);
                    animator.SetInteger("AnimState", 2);
                }
                else
                {
                    GameController.Instance.playerRB.velocity = new Vector2(0, playerRB.velocity.y);
                    animator.SetInteger("AnimState", 0);
                }
            }
        }
    }
    IEnumerator StayInDash()
    {
        if (!inDash)
        {
            float gravity = GameController.Instance.playerRB.gravityScale;
            GameController.Instance.playerRB.gravityScale = 0f;
            inDash = true;
            yield return new WaitForSeconds(0.15f);
            inDash = false;
            GameController.Instance.playerRB.gravityScale = gravity;
        }
    }

    //Getters and setters
    public int GetJumpCount()
    {
        return jumpCount;
    }

    public void SetJumpCount(int jumpCount)
    {
        this.jumpCount = jumpCount;
    }

    public int GetDashCount()
    {
        return dashCount;
    }

    public void SetDashCount(int dashCount)
    {
        this.dashCount = dashCount;
    }
    public int GetMaxJumpCount()
    {
        return maxJumpCount;
    }

    public void SetMaxJumpCount(int maxJumpCount)
    {
        this.maxJumpCount = maxJumpCount;
    }
    public int GetMaxDashCount()
    {
        return maxDashCount;
    }

    public void SetMaxDashCount(int maxDashCount)
    {
        this.maxDashCount = maxDashCount;
    }
}
