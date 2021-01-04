using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    public PlayerData playerData;
    public string prevWall;
    public LayerMask platformMask;
    public LayerMask wallMask;

    void Start()
    {
        prevWall = "";
    }
    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(playerData.boxCollider.bounds.center, playerData.boxCollider.bounds.size, 0f, Vector2.down, 1f, platformMask);
        return hit.collider != null;
    }
    public void Jump() // мы вызовем этот метод с события, см. InputManager
    {         
        if (IsGrounded())
        {
            playerData.rigidBody.velocity = Vector2.up * playerData.jumpSpeed;
            playerData.animator.SetInteger("AnimState", 3);
            Debug.Log("Jump!"); 
        }
        else if (playerData.jumpCount < playerData.maxJumpCount && !IsWallJump())
        {
            playerData.rigidBody.velocity = new Vector2(playerData.rigidBody.velocity.x, playerData.jumpSpeed);
            playerData.jumpCount++;
            Debug.Log("Double Jump!"); 
        }        
    }

    public bool IsWallJump()
    {
        RaycastHit2D hit = Physics2D.BoxCast(playerData.boxCollider.bounds.center, playerData.boxCollider.bounds.size, 0f, Vector2.right, 0.5f, wallMask);
        if (hit.collider != null)
            if (prevWall != hit.collider.gameObject.name)
            {
                playerData.rigidBody.velocity = Vector2.up * playerData.jumpSpeed + Vector2.left * playerData.jumpSpeed / 2;
                prevWall = hit.collider.gameObject.name;
                Debug.Log("Wall Jump!"); 
            }
            else
            {
                return false;
            }
        return hit.collider != null;
    }
}
