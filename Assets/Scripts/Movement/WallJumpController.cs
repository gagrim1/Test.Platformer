using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WallJumpController : MonoBehaviour
{
    [HideInInspector]
    public PlayerData playerData;
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
        //playerData.isOnWall = false;
        //RaycastHit2D hit = Physics2D.BoxCast(playerData.boxCollider.bounds.center, playerData.boxCollider.bounds.size + Vector3.right * 0.1f, 0f, Vector2.zero, 0f, wallMask);
        Collider2D[] walls = Physics2D.OverlapBoxAll(transform.position +
                new Vector3(playerData.boxCollider.offset.x * transform.localScale.x,
                playerData.boxCollider.offset.y * transform.localScale.y),
                (playerData.boxCollider.size + coonectToWallDist) * 2, 0f, wallMask);
        if (walls.Length == 0) {
            playerData.isOnWall = false;
            prevWall = null;
            return;
        }

        if (!walls.Contains(prevWall))
        {
            Debug.Log("jump");
            playerData.isOnWall = true;
            StartCoroutine(StayOnWall());
            prevWall = walls[0];
            //StartCoroutine(StayOnWall());
        }
        /*if (prevWall != hit.collider)
        {
            playerData.isOnWall = true;
            prevWall = hit.collider;
            Jump();
        }
        else
        {
            return false;
        }*/
        /*else
        {
            prevWall = null;
        }*/
        //return hit.collider != null;
    }

    public void Jump()
    {
        if (!playerData.isOnWall) return;

        playerData.isOnWall = false;
        playerData.soundManager.PlayWallJump();
        playerData.rigidBody.velocity = Vector2.up * playerData.jumpSpeed * 1;// + Vector2.left * playerData.jumpSpeed / 2;
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
            Gizmos.DrawWireCube(transform.position +
                new Vector3(playerData.boxCollider.offset.x * transform.localScale.x,
                playerData.boxCollider.offset.y * transform.localScale.y),
                (playerData.boxCollider.size + coonectToWallDist) * 2);
        }
    }
}
