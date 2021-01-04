using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    UnityEvent leftInputEvent;
    UnityEvent rightInputEvent;
    UnityEvent jumpInputEvent;
    UnityEvent dashInputEvent;

    void Start()
    {
        if (leftInputEvent == null) leftInputEvent = new UnityEvent();

        if (rightInputEvent == null) rightInputEvent = new UnityEvent();

        if (jumpInputEvent == null) jumpInputEvent = new UnityEvent();

        if (dashInputEvent == null) dashInputEvent = new UnityEvent();
    }
    
    void Update()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && leftInputEvent != null)
        {
            leftInputEvent.Invoke();
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && rightInputEvent != null)
        {
            rightInputEvent.Invoke();
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpInputEvent != null)
        {
            jumpInputEvent.Invoke();
        }
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q)) && dashInputEvent != null)
        {
            dashInputEvent.Invoke();
        }
    }
}
