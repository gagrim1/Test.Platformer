using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDawnController : MonoBehaviour
{
    [HideInInspector]
    public PlayerData playerData;
    [HideInInspector]
    public MovementManager _move;
    public void FallDawn()
    {
        if(playerData.isGrounded)
        {
            StartCoroutine(jumpDown());
        }
    }
    IEnumerator jumpDown()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("OneWayPlatform"), true);
        yield return new WaitForSeconds(0.15f);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("OneWayPlatform"), false);
    }
}
