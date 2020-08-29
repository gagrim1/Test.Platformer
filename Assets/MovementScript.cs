using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField]
    private LayerMask platformMask;
    private Rigidbody2D playerRB;
    private BoxCollider2D playerBC;

    private void Start()
    {
        playerBC = transform.GetComponent<BoxCollider2D>();
        playerRB = transform.GetComponent<Rigidbody2D>();
    }

    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(playerBC.bounds.center, playerBC.bounds.size, 0f, Vector2.down, 1f, platformMask);
        return hit.collider != null;
    }

    void Update()
    {
        float moveSpeed = 20f;
        if((Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.Space))&& isGrounded())
        {
            float jumpVelocity = 35f;
            playerRB.velocity = Vector2.up * jumpVelocity;
        }
         else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerRB.velocity = new Vector2(-moveSpeed, playerRB.velocity.y);
        }else {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                playerRB.velocity = new Vector2(+moveSpeed, playerRB.velocity.y);
            }else {
                playerRB.velocity = new Vector2(0, playerRB.velocity.y);
            }
        }
    }
}
