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
    public FallDawnController _fallDawn;

    public UnityEvent groundedEvent;
    public UnityEvent getControllEvent;
    public UnityEvent loseControllEvent;

    public GameObject level;

    public List<Collider2D> GroundColliders = new List<Collider2D>();

    void Start()
    {
        level = transform.parent.gameObject;
        prevWall = "";
        _walk.playerData = playerData;
        _walk._move = _jump._move = _dash._move = _fallDawn._move = gameObject.GetComponent<MovementManager>();
        _jump.playerData = playerData;
        _dash.playerData = playerData;
        _fallDawn.playerData = playerData;

        playerData.isGrounded = true;
        playerData.isControlled = true;

        if(groundedEvent == null) groundedEvent = new  UnityEvent();
        groundedEvent.AddListener(_walk.ChangeGroundedStatus);
        if(getControllEvent == null) getControllEvent = new  UnityEvent();
        getControllEvent.AddListener(GetControll);
        if(loseControllEvent == null) loseControllEvent = new  UnityEvent();
        loseControllEvent.AddListener(LoseControll);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Platform") || collision.gameObject.layer == LayerMask.NameToLayer("OneWayPlatform")) && !GroundColliders.Contains(collision.collider))
            foreach (var p in collision.contacts)
                if (p.point.y < playerData.boxCollider.bounds.min.y && !(p.point.x < playerData.boxCollider.bounds.min.x || p.point.x > playerData.boxCollider.bounds.max.x))
                {
                    GroundColliders.Add(collision.collider);
                    break;
                }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (GroundColliders.Contains(collision.collider))
            GroundColliders.Remove(collision.collider);
    }

    void LoseControll()
    {
        playerData.isControlled = false;
    }

    void GetControll()
    {
        playerData.isControlled = true;
        level.GetComponent<InputManager>().ReloadDir();
    }

    void Update()
    {
        bool newIsGrounded = IsGrounded();
        if (newIsGrounded != playerData.isGrounded)
        {
            playerData.animator.SetBool("IsGrounded", newIsGrounded);
            playerData.isGrounded = newIsGrounded;
            groundedEvent.Invoke();
        }
        if(playerData.isGrounded)
        {
            prevWall = null;
            playerData.jumpCount = 0;
            playerData.dashCount = 0;
        }
        IsWallJump();
    }

    public bool IsGrounded()
    {
        if (GroundColliders.Count > 0)
            return playerData.rigidBody.IsTouchingLayers(platformMask);
        else
            return false;
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
        RaycastHit2D hit = Physics2D.BoxCast(playerData.boxCollider.bounds.center, playerData.boxCollider.bounds.size+Vector3.right*0.1f, 0f, Vector2.zero, 0f, wallMask);
        if (hit.collider != null)
            if (prevWall != hit.collider.gameObject.name && !playerData.isGrounded)
            {
                playerData.rigidBody.velocity = Vector2.up * playerData.jumpSpeed*1;// + Vector2.left * playerData.jumpSpeed / 2;
                prevWall = hit.collider.gameObject.name;
            }
            else
            {
                return false;
            }
        return hit.collider != null;
    }
}
