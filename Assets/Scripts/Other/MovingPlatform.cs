using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject endObj;
    public GameObject startObj;
    private Vector2 end;
    private Vector2 start;
    private Vector2 now;
    private Vector2 move;
    private Rigidbody2D rigidBody;
    public float speed;
    public bool direction=true;
    public List<Rigidbody2D> passengers;

    private void Start()
    {
        rigidBody = GetComponentInChildren<Rigidbody2D>();
        transform.position = startObj.transform.position;
        //Destroy(endObj);
    }
    private void FixedUpdate()
    {
        start = startObj.transform.position;
        end = endObj.transform.position;
        now = transform.position;
        if (direction)
        {
            if (Vector2.Distance(now, end) <= speed * Time.fixedDeltaTime)
                direction = false;
            move = (end - start) / Vector2.Distance(start, end) * Mathf.Clamp(speed * Time.fixedDeltaTime, 0f, Vector2.Distance(now, end));
            //rigidBody.MovePosition(Vector2.Lerp(start, end, interpolate));
            //transform.position = now + (end - start) / Vector2.Distance(start, end) * Mathf.Clamp(speed * Time.fixedDeltaTime, 0f, Vector2.Distance(now, end));
        }
        else
        {
            if (Vector2.Distance(now, start) <= speed * Time.fixedDeltaTime)
                direction = true;
            move = -(end - start) / Vector2.Distance(start, end) * Mathf.Clamp(speed * Time.fixedDeltaTime, 0f, Vector2.Distance(now, start));
            //rigidBody.MovePosition(Vector2.Lerp(end, start, interpolate));
            //transform.position = now - (end - start) / Vector2.Distance(start, end) * Mathf.Clamp(speed * Time.fixedDeltaTime, 0f, Vector2.Distance(now, start));
        }
        rigidBody.MovePosition(now + move);
        passengers.ForEach(Move);
    }
    private void Move(Rigidbody2D passenger)
    {
        passenger.transform.position=(Vector2)passenger.transform.position + move;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        passengers.Add(collision.gameObject.GetComponent<Rigidbody2D>());
        //collision.gameObject.transform.parent = transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        passengers.Remove(collision.gameObject.GetComponent<Rigidbody2D>());
        //collision.gameObject.transform.parent = null;
    }
}
