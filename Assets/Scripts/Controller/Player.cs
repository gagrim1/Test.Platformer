using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private HealthSystem healthSystem;
    private Animator animator;
    private MovementScript moove;
    private GameObject player;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        animator = GetComponent<Animator>();
        moove = GetComponent<MovementScript>();
        moove.SetMaxDashCount(1);
        moove.SetMaxJumpCount(1);
        healthSystem = new HealthSystem(100);
    }
    void Start()
    {
       
    }
    void Update()
    {
        if (healthSystem.GetHealthAmount() > 0)
        {
            moove.Flip();
            if (moove.IsGrounded())
            {
                moove.SetJumpCount(0);
                moove.SetDashCount(0);
                animator.SetTrigger("Grounded");
            }
            else
            {
                animator.SetTrigger("Jump");
            }
            moove.JumpController();
            moove.DashController();
            moove.MovementController();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //healthSystem.Damage(10);
            GetDamage(10);
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

    public void DamageKnockBack(Vector2 knockBackDir, float knockBackDistance)
    {
        moove.playerRB.velocity += knockBackDir * knockBackDistance;
        
    }

    public void GetDamage(int damage)
    {
            healthSystem.Damage(damage);
    }

    public Vector3 GetPosition()
    {
        return player.transform.position;
    }

}
