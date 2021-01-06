using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    [HideInInspector]
    public PlayerData playerData;
    [HideInInspector]
    public MovementManager _move;

    public void Dash() // мы вызовем этот метод с события, см. InputManager
    {
        bool canDash = playerData.dashCount < playerData.maxDashCount && playerData.isControlled;
        if(canDash)
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
        _move.loseControllEvent.Invoke();
        yield return new WaitForSeconds(playerData.dashTime);
        playerData.rigidBody.gravityScale = gravity;
        _move.getControllEvent.Invoke(); 
    }
}
