using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Animator animator;

    [SerializeField]
    public LayerMask platformMask;
    [SerializeField]
    public LayerMask wallMask;

    bool canDamage;

    public string prevWall = "";
    public Rigidbody2D playerRB;
    public BoxCollider2D playerBC;

    public int jumpCount;
    public int dashCount;
    public int maxJumpCount;
    public int maxDashCount;
    [SerializeField]
    public float scale = 2;
    public bool inDash = false;

    public void Awake()
    {
        playerBC = transform.GetComponent<BoxCollider2D>();
        playerRB = transform.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        maxJumpCount = 1;
        maxDashCount = 1;
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(playerBC.bounds.center, playerBC.bounds.size, 0f, Vector2.down, 1f, platformMask);
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
                    playerRB.velocity = Vector2.up * jumpVelocity;
                    animator.SetInteger("AnimState", 3);
                }
                else
                if (jumpCount < maxJumpCount && !IsWallJump())
                {
                    playerRB.velocity = new Vector2( playerRB.velocity.x, jumpVelocity);
                    jumpCount++;
                }
            }

    }

    public bool IsWallJump()
    {
        float jumpVelocity = 35f;

        RaycastHit2D hit = Physics2D.BoxCast( playerBC.bounds.center,  playerBC.bounds.size, 0f, Vector2.right, 0.5f, wallMask);
        if (hit.collider != null)
            if (prevWall != hit.collider.gameObject.name)
            {
                 playerRB.velocity = Vector2.up * jumpVelocity + Vector2.left * jumpVelocity / 2;
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
                 playerRB.velocity = new Vector2(-transform.localScale.x / scale, 0) * dashVelocity;
            }
            else
            if (dashCount < maxDashCount)
            {
                StartCoroutine(StayInDash());
                 playerRB.velocity = new Vector2(-transform.localScale.x / scale, 0) * dashVelocity;
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
                 playerRB.velocity = new Vector2(-moveSpeed, playerRB.velocity.y);
                animator.SetInteger("AnimState", 2);
            }
            else
            {
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                     playerRB.velocity = new Vector2(+moveSpeed, playerRB.velocity.y);
                    animator.SetInteger("AnimState", 2);
                }
                else
                {
                     playerRB.velocity = new Vector2(0, playerRB.velocity.y);
                    animator.SetInteger("AnimState", 0);
                }
            }
        }
    }
    IEnumerator StayInDash()
    {
        if (!inDash)
        {
            float gravity =  playerRB.gravityScale;
             playerRB.gravityScale = 0f;
            inDash = true;
            yield return new WaitForSeconds(0.15f);
            inDash = false;
             playerRB.gravityScale = gravity;
        }
    }
}
