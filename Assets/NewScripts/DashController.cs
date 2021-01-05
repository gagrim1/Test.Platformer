using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    public PlayerData playerData;
    public SettingsData settingsData;
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
    public void Dash()
    {
        if(playerData.isInDash)
        {
            return;
        }
        if (playerData.dashCount < playerData.maxDashCount)
        {
            StartCoroutine(StayInDash());
            playerData.dashCount++;
        }
    }
    IEnumerator StayInDash()
    {
        Debug.Log("Dash!");
        float gravity =  playerData.rigidBody.gravityScale;
        playerData.rigidBody.gravityScale = 0f;
        playerData.rigidBody.velocity = new Vector2(-transform.localScale.x / scale, 0) * playerData.dashSpeed;
        playerData.isInDash = true;
        yield return new WaitForSeconds(0.15f);
        playerData.isInDash = false;
        playerData.rigidBody.gravityScale = gravity; 
    }
}
