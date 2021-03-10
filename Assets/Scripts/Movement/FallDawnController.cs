using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDawnController : MonoBehaviour
{
    [HideInInspector]
    public PlayerData playerData;
    [HideInInspector]
    public MovementManager _move;
    public Collider2D[] localGroupColliders;
    public void FallDown()
    {
        if (!playerData.isControlled) return;
        if (playerData.isGrounded && !playerData.isOnWall)
        {
            StartCoroutine(jumpDown());
        }
    }
    IEnumerator jumpDown()
    {
        localGroupColliders = _move.GroundColliders.ToArray();
        foreach (Collider2D collider in localGroupColliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("OneWayPlatform")
                || collider.gameObject.layer == LayerMask.NameToLayer("DestroyingPlatform")) 
            {
                collider.GetComponent<PlatformEffector2D>().colliderMask -= (int) Mathf.Pow(2,LayerMask.NameToLayer("Player"));
            }
        }
        yield return new WaitForSeconds(0.2f);
        foreach (Collider2D collider in localGroupColliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("OneWayPlatform")
                || collider.gameObject.layer == LayerMask.NameToLayer("DestroyingPlatform")) 
            {
                collider.GetComponent<PlatformEffector2D>().colliderMask += (int)Mathf.Pow(2, LayerMask.NameToLayer("Player"));
            }
        }
    }
}
