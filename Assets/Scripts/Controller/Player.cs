using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private MovementScript moove;
    private GameObject player;
    [SerializeField]
    private Database database;

    private void Awake()
    {
//        gameObject.AddComponent<HealthSystem>();
        GameController.Instance.playerHealthSystem = GetComponent<HealthSystem>();
        //healthSystem = GetComponent<HealthSystem>();
        animator = GetComponent<Animator>();
        moove = GetComponent<MovementScript>();
        moove.SetMaxDashCount(1);
        moove.SetMaxJumpCount(1);
        //GameController.Instance.playerHealthSystem = new HealthSystem(database.playerData.playerHP);
    }
    void Update()
    {
        if (GameController.Instance.playerHealthSystem.GetHealthAmount() > 0)
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
            moove.IsWallJump();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GetDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameController.Instance.playerHealthSystem.Heal(10);
        }
        if (GameController.Instance.playerHealthSystem.GetHealthAmount() == 0)
        {
            animator.SetTrigger("Death");
        }
}

    public void DamageKnockBack(Vector2 knockBackDir, float knockBackDistance)
    {
        moove.playerRB.velocity += knockBackDir * knockBackDistance;
        
    }

    public void GetDamage(int damage)
    {
            GameController.Instance.playerHealthSystem.Damage(damage);
    }

    public Vector3 GetPosition()
    {
        return player.transform.position;
    }

}
