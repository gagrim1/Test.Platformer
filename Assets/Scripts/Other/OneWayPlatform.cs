using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private BoxCollider2D platformCollider;
    private GameObject platform;
    void Start()
    {
        platformCollider = GetComponentInChildren<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Physics2D.IgnoreCollision(platformCollider, collision, true);
        Debug.Log("enter: " + platformCollider.gameObject.name + " " + collision.gameObject.name);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Physics2D.IgnoreCollision(platformCollider, collision, false);
        Debug.Log("exit:  " + platformCollider.gameObject.name + " " + collision.gameObject.name);
    }
}
