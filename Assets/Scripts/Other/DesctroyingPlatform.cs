using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesctroyingPlatform : MonoBehaviour
{
    public Collider2D platform;
    public SpriteRenderer sprite;
    private void Start()
    {
        platform = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.IsTouchingLayers(SortingLayer.NameToID("Player")))
        {
            StartCoroutine(Disapear());
        }
    }

    IEnumerator Disapear()
    {
        yield return new WaitForSeconds(2f);
        platform.enabled = false;
        sprite.enabled = false;
        yield return new WaitForSeconds(2f);
        platform.enabled = true;
        sprite.enabled = true;
    }
}
