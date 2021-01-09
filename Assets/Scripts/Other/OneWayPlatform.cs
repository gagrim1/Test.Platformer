using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private BoxCollider2D platformCollider;
    public MovementManager movementManager;
    void Start()
    {
        platformCollider = GetComponentInChildren<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.GetComponent<MovementManager>().IsGrounded())
        Physics2D.IgnoreCollision(platformCollider, collision, true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Physics2D.IgnoreCollision(platformCollider, collision, false);
    }
}
