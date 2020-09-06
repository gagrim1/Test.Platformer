using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Animator animator;
    private MovementScript moove;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        animator = GetComponent<Animator>();
        moove = GetComponent<MovementScript>();
        moove.SetMaxDashCount(1);
        moove.SetMaxJumpCount(1);
    }
    void Start()
    {
        healthSystem = new HealthSystem(100);
       
    }
    void Update()
    {
        if (healthSystem.GetHealthAmount() > 0)
        {
            moove.flip();
            if (moove.isGrounded())
            {
                moove.SetJumpCount(0);
                moove.SetDashCount(0);
                animator.SetTrigger("Grounded");
            }
            else
            {
                animator.SetTrigger("Jump");
            }
            moove.jumpController();
            moove.dashController();
            moove.movementController();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            healthSystem.Damage(10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            healthSystem.Heal(10);
        }
        if (healthSystem.GetHealthAmount() == 0)
        {
            animator.SetTrigger("Death");
            moove.StopMooving();
        }
}
}
