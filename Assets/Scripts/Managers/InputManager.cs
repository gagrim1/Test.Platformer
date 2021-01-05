using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public GameManager gameManager;
    UnityEvent<string> xAxisInputEvent;
    UnityEvent jumpInputEvent;
    UnityEvent dashInputEvent;

    void Start()
    {
        gameManager = transform.gameObject.GetComponent<GameManager>();

        if (xAxisInputEvent == null) xAxisInputEvent = new UnityEvent<string>();
        xAxisInputEvent.AddListener(gameManager.player.GetComponent<WalkController>().Move);

        if (jumpInputEvent == null) jumpInputEvent = new UnityEvent();
        jumpInputEvent.AddListener(gameManager.player.GetComponent<JumpController>().Jump);

        if (dashInputEvent == null) dashInputEvent = new UnityEvent();
        dashInputEvent.AddListener(gameManager.player.GetComponent<DashController>().Dash);
    }
    
    void Update()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && xAxisInputEvent != null)
        {
            xAxisInputEvent.Invoke("left");
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && xAxisInputEvent != null)
        {
            xAxisInputEvent.Invoke("right");
        }
        if ((   !Input.GetKey(KeyCode.D) 
            && !Input.GetKey(KeyCode.RightArrow) 
            && !Input.GetKey(KeyCode.A) 
            && !Input.GetKey(KeyCode.LeftArrow)) 
            || xAxisInputEvent == null)
        {
            xAxisInputEvent.Invoke("none");
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpInputEvent != null)
        {
            jumpInputEvent.Invoke();
        }
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q)) && dashInputEvent != null)
        {
            dashInputEvent.Invoke();
        }

        // TEST: delete after testing
        if (Input.GetKeyDown(KeyCode.K))
        {
            gameManager.player.GetComponent<HealthManager>().Damage(10);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            gameManager.player.GetComponent<HealthManager>().Heal(7);
        }
    }
}
