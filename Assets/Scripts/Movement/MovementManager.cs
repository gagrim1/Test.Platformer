﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementManager : MonoBehaviour
{
    public PlayerData playerData;
    public LevelData levelData;
    public LayerMask platformMask;

    public WalkController _walk;
    public JumpController _jump;
    public DashController _dash;
    public FallDawnController _fallDawn;
    public WallJumpController _wallJump;

    public UnityEvent groundedEvent;
    public UnityEvent getControllEvent;
    public UnityEvent loseControllEvent;
    public UnityEvent wallJumpEvent;

    public GameObject level;

    public List<Collider2D> GroundColliders = new List<Collider2D>();

    void Start()
    {
        level = transform.parent.gameObject;
        levelData = level.GetComponent<GameManager>().gameData.levelData;
        _walk.playerData = playerData;
        _walk._move = _jump._move = _dash._move = _fallDawn._move = _wallJump.move = gameObject.GetComponent<MovementManager>();
        _jump.playerData = playerData;
        _dash.playerData = playerData;
        _fallDawn.playerData = playerData;
        _wallJump.playerData = playerData;

        playerData.isGrounded = true;
        playerData.isControlled = true;
        playerData.isPushed = false;

        if(groundedEvent == null) groundedEvent = new  UnityEvent();
        groundedEvent.AddListener(_walk.ChangeGroundedStatus);
        if(getControllEvent == null) getControllEvent = new  UnityEvent();
        getControllEvent.AddListener(GetControll);
        if(loseControllEvent == null) loseControllEvent = new  UnityEvent();
        loseControllEvent.AddListener(LoseControll);
        if (wallJumpEvent == null) wallJumpEvent = new UnityEvent();
        wallJumpEvent.AddListener(GetComponent<WallJumpController>().WallJump);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Platform") 
            || collision.gameObject.layer == LayerMask.NameToLayer("OneWayPlatform")
            || collision.gameObject.layer == LayerMask.NameToLayer("DestroyingPlatform")) 
            && !GroundColliders.Contains(collision.collider))
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

    public void LoseControll()
    {
        playerData.isControlled = false;
    }

    public void StopMoving()
    {
        playerData.rigidBody.velocity = new Vector2(0f, playerData.rigidBody.velocity.y);
    }


    public void GetControll()
    {
        if(playerData.isAlive)
            playerData.isControlled = true;
        else
            StopMoving();
        level.GetComponent<InputManager>().ReloadDir();
    }

    void Update()
    {
        bool newIsGrounded = IsGrounded();
        if (newIsGrounded != playerData.isGrounded)
        {
            playerData.isGrounded = newIsGrounded;
            groundedEvent.Invoke();
        }
        if (playerData.isGrounded)
        {
            playerData.jumpCount = 0;
            playerData.dashCount = 0;
        }
        wallJumpEvent.Invoke();
            playerData.rigidBody.velocity = new Vector2(playerData.rigidBody.velocity.x, 
                Mathf.Clamp(playerData.rigidBody.velocity.y, playerData.maxFallVelocity, 100f));

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
        if(!playerData.isAlive)
        {
            return;
        }
        Vector2 pushDirection = new Vector2(transform.localScale.x, 2.0f);
        pushDirection.Normalize();
        //StartCoroutine(StayInFall(0.2f));
        StartCoroutine(StayInPush());
        playerData.rigidBody.velocity = new Vector2(0f, 0f);
        playerData.rigidBody.AddForce(pushDirection * playerData.pushSpeed);
        
    }

    public void Respawn()
    {
        playerData.rigidBody.velocity = Vector2.zero;
        gameObject.transform.position = levelData.checkpoints[levelData.checkpoints.Count - 1].transform.position;
    }

    IEnumerator StayInFall(float time)
    {
        loseControllEvent.Invoke();
        yield return new WaitForSeconds(time);
        getControllEvent.Invoke();      
    }
    bool IsDontStop()
    {
        return playerData.isGrounded || !playerData.isPushed;
    }

    public IEnumerator StayInPush()
    {
        playerData.isPushed = true;
        yield return new WaitUntil(IsDontStop);
        playerData.isPushed = false;    
    }
}
