using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private RectTransform bounds;
    public bool isHorizontal;
    private Vector2 platformSize;
    private Vector2 start;
    private Vector2 end; 
    private Vector2 now;
    private Vector2 move;
    private Rigidbody2D rigidBody;
    public float speed;
    public bool direction=true;
    public List<Rigidbody2D> passengers;
    Transform lastParent;

    private void Start()
    {
        platformSize = gameObject.GetComponent<SpriteRenderer>().size;
        rigidBody = GetComponentInChildren<Rigidbody2D>();
        bounds = transform.parent.gameObject.GetComponent<RectTransform>() as RectTransform;
        float left = bounds.transform.position.x - bounds.rect.width / 2;
        float top = bounds.transform.position.y - bounds.rect.height / 2;
        float right = bounds.transform.position.x + bounds.rect.width / 2;
        float bottom = bounds.transform.position.y + bounds.rect.height / 2; 
        if(isHorizontal)
        {
            start.x = left + platformSize.x / 2; 
            end.x = right - platformSize.x / 2;
            start.y = end.y = transform.position.y;
        }  
        else 
        {
            start.y = bottom - platformSize.y / 2; 
            end.y = top + platformSize.y / 2;
            start.x = end.x = transform.position.x;
        }  
        transform.position = start;
    }

    private void FixedUpdate()
    {
        now = transform.position;
        if (direction)
        {
            if (Vector2.Distance(now, end) <= speed * Time.fixedDeltaTime)
                direction = false;
            move = (end - start) / Vector2.Distance(start, end) * Mathf.Clamp(speed * Time.fixedDeltaTime, 0f, Vector2.Distance(now, end));
        }
        else
        {
            if (Vector2.Distance(now, start) <= speed * Time.fixedDeltaTime)
                direction = true;
            move = -(end - start) / Vector2.Distance(start, end) * Mathf.Clamp(speed * Time.fixedDeltaTime, 0f, Vector2.Distance(now, start));
        }
        transform.position = now + move;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isTopCollision(collision))
        {
            return;
        }
        lastParent = collision.gameObject.GetComponent<MovementManager>().level.transform;
        collision.gameObject.transform.parent = transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = lastParent;
    }

    private bool isTopCollision(Collision2D collision)
    {
        Bounds platformColBounds = gameObject.GetComponent<Collider2D>().bounds;
        Bounds colBounds = collision.collider.bounds;
        float eps = 0.05f;
        if((colBounds.min.y + eps >= platformColBounds.max.y) && (colBounds.max.x + eps >= platformColBounds.min.x && colBounds.min.x - eps <= platformColBounds.max.x))
        {
            return true;
        }
        return false;
    }
}
