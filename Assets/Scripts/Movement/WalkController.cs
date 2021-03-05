using System;
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
            charScale.x = -Math.Abs(charScale.x);
        }
        if (direction == "left")
        {
            charScale.x = Math.Abs(charScale.x);
        }
        transform.localScale = charScale;
    }

    public void ChangeGroundedStatus()
    {
        playerData.animator.SetBool("IsGrounded", playerData.isGrounded);
        if (!playerData.isGrounded)
            playerData.soundManager.StopRun();
    }

    public void Move(string direction) // мы вызовем этот метод с события, см. InputManager
    {
        if(!playerData.isControlled)
        {
            return;
        }
        if (direction == "left")
        {   
            Flip(direction);
            playerData.rigidBody.velocity = new Vector2(-playerData.moveSpeed, playerData.rigidBody.velocity.y);
            playerData.animator.SetBool("Run", true);
            if (playerData.isGrounded)
            {
                playerData.soundManager.StartRun();
            }
        }
        else if (direction == "right")
        {
            Flip(direction);
            playerData.rigidBody.velocity = new Vector2(+playerData.moveSpeed, playerData.rigidBody.velocity.y);
            playerData.animator.SetBool("Run", true);
            if (playerData.isGrounded)
            {
                playerData.soundManager.StartRun();
            }
        } 
        else 
        {
            playerData.rigidBody.velocity = new Vector2(0, playerData.rigidBody.velocity.y);
            playerData.animator.SetBool("Run", false);
            playerData.soundManager.StopRun();
        } 
    }
}
