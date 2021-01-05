using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    [HideInInspector]
    public PlayerData playerData;
    [HideInInspector]
    public MovementManager _move;

    void Start()
    {
        playerData.isInDash = false;
    }

    public void Dash() // мы вызовем этот метод с события, см. InputManager
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
        float gravity =  playerData.rigidBody.gravityScale;
        playerData.rigidBody.gravityScale = 0f;
        playerData.rigidBody.velocity = new Vector2(-transform.localScale.x / playerData.scale, 0) * playerData.dashSpeed;
        playerData.isInDash = true;
        yield return new WaitForSeconds(playerData.dashTime);
        playerData.isInDash = false;
        playerData.rigidBody.gravityScale = gravity; 
    }
}
