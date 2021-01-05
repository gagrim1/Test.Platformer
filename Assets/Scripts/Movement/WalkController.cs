using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkController : MonoBehaviour
{
    [HideInInspector]
    public PlayerData playerData;
    [HideInInspector]
    public MovementManager _move;

    public void Flip(string direction)
    {
        Vector2 charScale = transform.localScale;
        if (direction == "right")
        {
            charScale.x = -playerData.scale;
        }
        if (direction == "left")
        {
            charScale.x = playerData.scale;
        }
        transform.localScale = charScale;
    }

    public void Move(string direction) // мы вызовем этот метод с события, см. InputManager
    {
        if(playerData.isInDash)
        {
            return;
        }
        if(_move.IsGrounded())
        {
            playerData.jumpCount = 0;
            playerData.dashCount = 0;
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
        transform.gameObject.GetComponent<JumpController>()._move.IsWallJump();
    }
}
