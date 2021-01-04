using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkController : MonoBehaviour
{
    public PlayerData playerData;
    public LayerMask platformMask;
    public float scale;

    void Start()
    {
        scale = 2;
    }
    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(playerData.boxCollider.bounds.center, playerData.boxCollider.bounds.size, 0f, Vector2.down, 1f, platformMask);
        return hit.collider != null;
    }

    public void Flip(string direction)
    {
        Vector2 charScale = transform.localScale;
        if (direction == "right")
        {
            charScale.x = -scale;
        }
        if (direction == "left")
        {
            charScale.x = scale;
        }
        transform.localScale = charScale;
    }

    public void Move(string direction)
    {
        if(IsGrounded())
        {
            playerData.jumpCount = 0;
            playerData.animator.SetTrigger("Grounded");
        }
        else 
        {
            playerData.animator.SetTrigger("Jump");
        }

        if (direction == "left")
        {   
            Flip(direction);
            playerData.rigidBody.velocity = new Vector2(-playerData.moveSpeed, playerData.rigidBody.velocity.y);
            playerData.animator.SetInteger("AnimState", 2);
        }
        else if (direction == "right")
        {
            Flip(direction);
            playerData.rigidBody.velocity = new Vector2(+playerData.moveSpeed, playerData.rigidBody.velocity.y);
            playerData.animator.SetInteger("AnimState", 2);
        } 
        else 
        {
            playerData.rigidBody.velocity = new Vector2(0, playerData.rigidBody.velocity.y);
            playerData.animator.SetInteger("AnimState", 0);
        }  
        transform.gameObject.GetComponent<JumpController>().IsWallJump();
    }
}
