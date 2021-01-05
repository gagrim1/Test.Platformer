using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [HideInInspector]
    public PlayerData playerData;
    [HideInInspector]
    public MovementManager _move;

    public void Jump() // мы вызовем этот метод с события, см. InputManager
    {         
        if (_move.IsGrounded())
        {
            playerData.rigidBody.velocity = Vector2.up * playerData.jumpSpeed;
            playerData.animator.SetInteger("AnimState", 3);
        }
        else if (playerData.jumpCount < playerData.maxJumpCount && !_move.IsWallJump())
        {
            playerData.rigidBody.velocity = new Vector2(playerData.rigidBody.velocity.x, playerData.jumpSpeed);
            playerData.jumpCount++;
        }        
    }
}
