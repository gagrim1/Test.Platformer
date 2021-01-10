using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject endObj;
    private Vector2 end;
    private Vector2 start;
    private Vector2 now;
    public float speed;
    public bool direction=true;
    private void Start()
    {
        start = transform.position;
        end = endObj.transform.position;
        Destroy(endObj);
    }

    void Update()
    {
        now = transform.position;
        if (direction) 
        {
            if (Vector2.Distance(now, end) <= speed * Time.deltaTime)
                direction = false;
            transform.position = now + (end - start) / Vector2.Distance(start, end) * Mathf.Clamp(speed * Time.deltaTime, 0f, Vector2.Distance(now, end));
        }
        else
        {
            if (Vector2.Distance(now, start) <= speed * Time.deltaTime)
                direction = true;
            transform.position = now - (end - start) / Vector2.Distance(start, end) * Mathf.Clamp(speed * Time.deltaTime, 0f, Vector2.Distance(now, start));
        }   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = null;
    }
}
