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

        Vector3 velocity = Vector3.zero;
        Vector3 targetVelocity = new Vector2(0f, playerData.rigidBody.velocity.y);

        if (direction == "left" || direction == "right")
        {   
            Flip(direction);
            playerData.animator.SetBool("Run", true);
            if (playerData.isGrounded)
            {
                playerData.soundManager.StartRun();
            }

            if(direction == "left")
            {
                targetVelocity.x = -playerData.moveSpeed;
            }
            else
            {
                targetVelocity.x = playerData.moveSpeed;
            }
            if(playerData.isPushed)
            {
                playerData.isPushed = false;
            }
        }
        else 
        {
            playerData.animator.SetBool("Run", false);
            playerData.soundManager.StopRun();
            if(playerData.isPushed)
            {
                targetVelocity.x = playerData.rigidBody.velocity.x;
            }
        } 
        playerData.rigidBody.velocity = Vector3.SmoothDamp(playerData.rigidBody.velocity, targetVelocity, ref velocity, 0f);

    }
}
