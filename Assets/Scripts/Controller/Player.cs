using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private MovementScript moove;

    private void Awake()
    {
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
