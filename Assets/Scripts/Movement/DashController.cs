using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    [HideInInspector]
    public PlayerData playerData;
    [HideInInspector]
    public MovementManager _move;
    private int direction;
    public void Dash(string dir) // мы вызовем этот метод с события, см. InputManager
    {

        switch (dir)
        {
            case "right": direction = 1;
                break;

            case "left":
                direction = -1;
                break;
            default:
                direction = 1;
                break;
        }
        bool canDash = playerData.dashCount < playerData.maxDashCount && playerData.isControlled;
        if(canDash)
        {
            _move._walk.Flip(dir);
            StartCoroutine(StayInDash());
            playerData.dashCount++;
        }
    }

    IEnumerator StayInDash()
    {
        float gravity =  playerData.rigidBody.gravityScale;
        playerData.rigidBody.gravityScale = 0f;
        playerData.rigidBody.velocity = new Vector2(direction, 0) * playerData.dashSpeed;
        playerData.animator.SetBool("Dash", true);
        _move.loseControllEvent.Invoke();
        yield return new WaitForSeconds(playerData.dashTime);
        playerData.animator.SetBool("Dash", false);
        playerData.rigidBody.gravityScale = gravity;
        _move.getControllEvent.Invoke(); 
    }
}
