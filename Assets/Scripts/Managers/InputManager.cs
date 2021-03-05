using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public GameManager gameManager;
    UnityEvent<string> xAxisInputEvent;
    UnityEvent jumpInputEvent;
    UnityEvent fallDawnInputEvent;
    UnityEvent attackInputEvent;
    UnityEvent<string> dashInputEvent;

    public enum Dir {Left, Right, None, Reload};
    public Dir dir;
    public bool reloading;

    void Start()
    {
        gameManager = transform.gameObject.GetComponent<GameManager>();

        if (xAxisInputEvent == null) xAxisInputEvent = new UnityEvent<string>();
        xAxisInputEvent.AddListener(gameManager.player.GetComponent<WalkController>().Move);

        if (jumpInputEvent == null) jumpInputEvent = new UnityEvent();
        jumpInputEvent.AddListener(gameManager.player.GetComponent<JumpController>().Jump);

        if (dashInputEvent == null) dashInputEvent = new UnityEvent<string>();
        dashInputEvent.AddListener(gameManager.player.GetComponent<DashController>().Dash);

        if (fallDawnInputEvent == null) fallDawnInputEvent = new UnityEvent();
        fallDawnInputEvent.AddListener(gameManager.player.GetComponent<FallDawnController>().FallDawn);

        if (attackInputEvent == null) attackInputEvent = new UnityEvent();
        attackInputEvent.AddListener(gameManager.player.GetComponent<AttackController>().Attack);

        dir = Dir.None;  
        reloading = true;      
    }
    
    public void ReloadDir()
    {
        dir = Dir.Reload;
    }

    void Update()
    {
        if(reloading){
            StartCoroutine(ReloadingTimer());
            ReloadDir();
            //Debug.Log("Reloading...");
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.R))
        {
            attackInputEvent.Invoke();
        } 

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && xAxisInputEvent != null)
        {
            if (dir != Dir.Left)
            {
                dir = Dir.Left;
                xAxisInputEvent.Invoke("left"); 
            }              
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && xAxisInputEvent != null)
        {
            if (dir != Dir.Right)
            {
                dir = Dir.Right;
                xAxisInputEvent.Invoke("right"); 
            }           
        }
        if ((   !Input.GetKey(KeyCode.D) 
            && !Input.GetKey(KeyCode.RightArrow) 
            && !Input.GetKey(KeyCode.A) 
            && !Input.GetKey(KeyCode.LeftArrow)) 
            || xAxisInputEvent == null)
        {
            if (dir != Dir.None)
            {
                dir = Dir.None;
                xAxisInputEvent.Invoke("none"); 
            } 
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpInputEvent != null)
        {
            jumpInputEvent.Invoke();
        }
        if((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && fallDawnInputEvent != null)
        {
            fallDawnInputEvent.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.E) && dashInputEvent != null)
        {
            dashInputEvent.Invoke("right");
        }
        if (Input.GetKeyDown(KeyCode.Q) && dashInputEvent != null)
        {
            dashInputEvent.Invoke("left");
        }

        // TEST: delete after testing
        if (Input.GetKeyDown(KeyCode.K))
        {
            gameManager.player.GetComponent<IHealthManager>().Damage(10);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            gameManager.player.GetComponent<IHealthManager>().Heal(7);
        }
    }

    IEnumerator ReloadingTimer()
    {   
        reloading = false;
        yield return new WaitForSeconds(0.1f);
        reloading = true;
    }

}
