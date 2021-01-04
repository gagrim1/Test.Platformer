using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    
    private Animator animator;
    private MovementScript moove;

    private void Awake()
    {
        gameManager = transform.parent.gameObject.GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        moove = GetComponent<MovementScript>();
        moove.maxDashCount = 1;
        moove.maxJumpCount = 1;
    }

    void Update()
    {
        
            moove.Flip();
            if (moove.IsGrounded())
            {
                moove.jumpCount = 0;
                moove.dashCount = 0;
                animator.SetTrigger("Grounded");
            }
            else
            {
                animator.SetTrigger("Jump");
            }
            moove.JumpController();
            moove.DashController();
            moove.MovementController();
            moove.IsWallJump();
        
    }
}
