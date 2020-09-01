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
    private Rigidbody2D playerRB;
    private BoxCollider2D playerBC;
    private SpriteRenderer spriteRenderer;

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
        animator = GetComponent<Animator>();
        maxJumpCount = 1;
        maxDashCount = 1;
    }
    private void Start()
    {

    }

    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(playerBC.bounds.center, playerBC.bounds.size, 0f, Vector2.down, 1f, platformMask);
        return hit.collider != null;
    }

    void Update()
    {
        flip();
        if (isGrounded())
        {
            jumpCount = 0;
            dashCount = 0;
            animator.SetTrigger("Grounded");
            prevWall = "";
        }
        else {
            animator.SetTrigger("Jump");
        }
        jumpController();
        dashController();
        movementController();
    }

    void jumpController()
    {
        float jumpVelocity = 35f;
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            if (isGrounded())
            {
                playerRB.velocity = Vector2.up * jumpVelocity;
            }
            else
            if (jumpCount < maxJumpCount&&!isWallJump())
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpVelocity);
                jumpCount++;
            }
        }

    }

    bool isWallJump()
    {
        float jumpVelocity = 35f;
        if (isOnRightWall())
        {
            playerRB.velocity = Vector2.up * jumpVelocity + Vector2.left * jumpVelocity / 2;
            return true;
        }
        else
        if (isOnLeftWall())
        {
            playerRB.velocity = Vector2.up * jumpVelocity + Vector2.right * jumpVelocity / 2;
            return true;
        }
        return false;
    }

    void dashController()
    {
        float dashVelocity = 45f;
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
        {
            if (isGrounded())
            {
                StartCoroutine(stayInDash());
                playerRB.velocity = new Vector2(-transform.localScale.x / scale, 0) * dashVelocity;
            }
            else
            if (dashCount < maxDashCount)
            {
                StartCoroutine(stayInDash());
                playerRB.velocity = new Vector2(-transform.localScale.x / scale, 0) * dashVelocity;
                dashCount++;
            }
        }
    }

    void flip() {
        Vector2 charScale = transform.localScale;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            charScale.x = -scale;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            charScale.x = scale;
        }
        transform.localScale = charScale;
    }
    
    bool isOnRightWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(playerBC.bounds.center, playerBC.bounds.size, 0f, Vector2.right, 0.5f, wallMask);
        if (hit.collider != null)
            if (prevWall != hit.collider.gameObject.name) {
                prevWall = hit.collider.gameObject.name;
            }
            else
            {
                return false;
            }
        return hit.collider != null;
    }
    bool isOnLeftWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(playerBC.bounds.center, playerBC.bounds.size, 0f, Vector2.left, 0.5f, wallMask);
        if (hit.collider != null)
            if (prevWall != hit.collider.gameObject.name)
            {
                prevWall = hit.collider.gameObject.name;
            }
            else
            {
                return false;
            }
        return hit.collider != null;
    }

    void movementController()
    {
        float moveSpeed = 20f;
        if(!inDash)
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (isGrounded())
            {
                playerRB.velocity = new Vector2(-moveSpeed, playerRB.velocity.y);
                animator.SetInteger("AnimState", 2);
            }
            else {
                float midAirControl = 3f;
                playerRB.velocity += new Vector2(-moveSpeed * midAirControl * Time.deltaTime, 0);
                playerRB.velocity = new Vector2(Mathf.Clamp(playerRB.velocity.x, -moveSpeed, +moveSpeed), playerRB.velocity.y);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (isGrounded())
                {
                    playerRB.velocity = new Vector2(+moveSpeed, playerRB.velocity.y);
                    animator.SetInteger("AnimState", 2);
                }
                else {
                    float midAirControl = 3f;
                    playerRB.velocity += new Vector2(+moveSpeed * midAirControl * Time.deltaTime, 0);
                    playerRB.velocity = new Vector2(Mathf.Clamp(playerRB.velocity.x, -moveSpeed, +moveSpeed), playerRB.velocity.y);
                }
            }
            else
            {
                if (isGrounded())
                {
                    playerRB.velocity = new Vector2(0, playerRB.velocity.y);
                }
                animator.SetInteger("AnimState", 0);
            }
        }
    }
    IEnumerator stayInDash()
    {
        if (!inDash)
        {
            float gravity = playerRB.gravityScale;
            playerRB.gravityScale = 0f;
            inDash = true;
            Debug.Log("started");
            yield return new WaitForSeconds(0.15f);
            Debug.Log("ended");
            inDash = false;
            playerRB.gravityScale = gravity;
        }
    }
}
