using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementManager : MonoBehaviour
{
    public PlayerData playerData;
    public string prevWall;
    public LayerMask platformMask;
    public LayerMask wallMask;

    public WalkController _walk;
    public JumpController _jump;
    public DashController _dash;

    public UnityEvent groundedEvent;
    public UnityEvent getControllEvent;
    public UnityEvent loseControllEvent;

    void Start()
    {
        prevWall = "";
        _walk.playerData = playerData;
        _walk._move = _jump._move = _dash._move = gameObject.GetComponent<MovementManager>();
        _jump.playerData = playerData;
        _dash.playerData = playerData;

        playerData.isGrounded = true;
        playerData.isControlled = true;

        if(groundedEvent == null) groundedEvent = new  UnityEvent();
        groundedEvent.AddListener(_walk.ChangeGroundedStatus);
        if(getControllEvent == null) getControllEvent = new  UnityEvent();
        getControllEvent.AddListener(GetControll);
        if(loseControllEvent == null) loseControllEvent = new  UnityEvent();
        loseControllEvent.AddListener(LoseControll);
    }

    void LoseControll()
    {
        playerData.isControlled = false;
    }

    void GetControll()
    {
        playerData.isControlled = true;
        transform.parent.gameObject.GetComponent<InputManager>().ReloadDir();
    }

    void Update()
    {
        bool newIsGrounded = IsGrounded();
        if (newIsGrounded != playerData.isGrounded)
        {
            playerData.isGrounded = newIsGrounded;
            groundedEvent.Invoke();
        }
        if(playerData.isGrounded)
        {
            playerData.jumpCount = 0;
            playerData.dashCount = 0;
        }
        IsWallJump();
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(playerData.boxCollider.bounds.center, playerData.boxCollider.bounds.size, 0f, Vector2.down, 1f, platformMask);
        return hit.collider != null;
    }

    public void DamagedPush(Vector2 from)
    {   
        Vector2 pushDirection = 2 * Vector2.up;
        if(from.x < transform.position.x)
        {
            pushDirection += Vector2.right;
        }
        else
        {
            pushDirection += Vector2.left;
        }
        StartCoroutine(StayInFall());
        playerData.rigidBody.velocity = pushDirection * playerData.pushSpeed; 
    }

    IEnumerator StayInFall()
    {
        loseControllEvent.Invoke();
        yield return new WaitForSeconds(0.5f);
        getControllEvent.Invoke();      
    }

    public bool IsWallJump()
    {
        RaycastHit2D hit = Physics2D.BoxCast(playerData.boxCollider.bounds.center, playerData.boxCollider.bounds.size, 0f, Vector2.right, 0.5f, wallMask);
        if (hit.collider != null)
            if (prevWall != hit.collider.gameObject.name)
            {
                playerData.rigidBody.velocity = Vector2.up * playerData.jumpSpeed + Vector2.left * playerData.jumpSpeed / 2;
                prevWall = hit.collider.gameObject.name;
            }
            else
            {
                return false;
            }
        return hit.collider != null;
    }
}
