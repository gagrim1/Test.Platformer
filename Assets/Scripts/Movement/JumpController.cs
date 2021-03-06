﻿using System.Collections;
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
        bool canJump = (playerData.jumpCount < playerData.maxJumpCount) && !playerData.isOnWall && playerData.isControlled;  
        if (canJump)
        {
            if (playerData.isGrounded)
                playerData.soundManager.PlayJump();
            else
                playerData.soundManager.PlayAirJump();

            playerData.rigidBody.velocity = new Vector2(playerData.rigidBody.velocity.x, 0);
            playerData.rigidBody.AddForce(new Vector2(0, playerData.jumpSpeed));
            playerData.jumpCount++;
        }       
    }
}
