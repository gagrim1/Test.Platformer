using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WallJumpController : MonoBehaviour
{
    [HideInInspector]
    public PlayerData playerData;
    public MovementManager move;
    private Collider2D prevWall=null;
    public LayerMask wallMask;
    private Vector2 coonectToWallDist = new Vector2(0.2f, 0f);
    public void WallJump()
    {
        if (playerData.isGrounded)
        {
            playerData.isOnWall = false;
            prevWall = null;
            return;
        }
        else CheckWallJump();
            //Jump();
    }

    public void CheckWallJump()
    {
        Collider2D[] walls = Physics2D.OverlapBoxAll(GetWallJumpCheckBoxPos(), GetWallJumpCheckBoxSize(), 0f, wallMask);
        if (walls.Length == 0) {
            playerData.isOnWall = false;
            prevWall = null;
            return;
        }

        if (!walls.Contains(prevWall))
        {
            playerData.isOnWall = true;
            playerData.isPushed = false;
            if (transform.position.x < walls[0].transform.position.x)
                move._walk.Flip("right");
            else
                move._walk.Flip("left");
            StartCoroutine(StayOnWall());
            prevWall = walls[0];

        }
    }

    public void Jump()
    {
        if (!playerData.isOnWall) return;

        //StartCoroutine(move.StayInPush());
        playerData.isOnWall = false;
        playerData.soundManager.PlayWallJump();
        playerData.rigidBody.velocity = new Vector2(0f, 0f);
        if (transform.localScale.x > 0)
            move._walk.Flip("right");
        else
            move._walk.Flip("left");
        Vector2 jumpDir = new Vector2(0, 2f).normalized;
        playerData.rigidBody.AddForce(jumpDir * playerData.jumpSpeed);
        //playerData.rigidBody.velocity = Vector2.up * playerData.jumpSpeed * 1;// + Vector2.left * playerData.jumpSpeed / 2;
    }

    IEnumerator StayOnWall()
    {
        playerData.animator.SetBool("IsOnWall", true);
        playerData.maxFallVelocity = playerData.maxFallVelocityOnWall;
        yield return new WaitWhile(IsOnWall);
        playerData.animator.SetBool("IsOnWall", false);
        playerData.maxFallVelocity = playerData.maxFallVelocityUsual;
    }

    private bool IsOnWall() { return playerData.isOnWall; }

    private void OnDrawGizmosSelected() 
    {
        if (playerData != null && playerData.boxCollider != null)
        {
            Gizmos.DrawWireCube(GetWallJumpCheckBoxPos(), GetWallJumpCheckBoxSize());
        }
    }

    private Vector3 GetWallJumpCheckBoxPos() 
    {
        return transform.position +
                new Vector3(playerData.boxCollider.offset.x * 
                transform.localScale.x,
                playerData.boxCollider.offset.y * 
                transform.localScale.y);
    }

    private Vector2 GetWallJumpCheckBoxSize()
    {
        return (new Vector2(playerData.boxCollider.size.x, playerData.boxCollider.size.y/2) + coonectToWallDist) * transform.localScale.y;
    }
}
