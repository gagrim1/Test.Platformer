using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    private LayerMask platformMask;
    private Rigidbody2D playerRB;
    private BoxCollider2D playerBC;
    private SpriteRenderer spriteRenderer;

    private int countJump;
    private int countJumpMax;

    private void Start()
    {
        playerBC = transform.GetComponent<BoxCollider2D>();
        playerRB = transform.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        countJumpMax = 1;
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
            countJump = 0;
            animator.SetTrigger("Grounded");
        }
        else {
            animator.SetTrigger("Jump");
        }
        jumpController();
        movementController();
    }

    void jumpController()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)))
        {
            if (isGrounded())
            {
                float jumpVelocity = 35f;
                playerRB.velocity = Vector2.up * jumpVelocity;
            }
            else {
                if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))) {
                    if (countJump < countJumpMax) {
                        float jumpVelocity = 30f;
                        playerRB.velocity = Vector2.up * jumpVelocity;
                        countJump++;
                    }
                }
            }
        }
    }

    void flip() {
        Vector2 charScale = transform.localScale;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            charScale.x = -2;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            charScale.x = 2;
        }
        transform.localScale = charScale;
    }

    void movementController()
    {
        float moveSpeed = 20f;

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
                playerRB.velocity = new Vector2(0, playerRB.velocity.y);
                animator.SetInteger("AnimState", 0);
            }
        }
    }
}
