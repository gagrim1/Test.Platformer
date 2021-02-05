using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public PlayerData playerData;
    public Transform attackPoint;
    public MovementManager movement;
    public float attackRange;
    public LayerMask enemyLayers;
    private Collider2D[] enemies;
    public void Attack()
    {
        //play animation
        playerData.animator.SetTrigger("Attack");
        StartCoroutine(stayInAttack());
        //check enemys
        enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //hurt enemys
        foreach(Collider2D enemy in enemies)
        {
            Debug.Log(enemy.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator stayInAttack()
    {
        movement.StopMoving();
        movement.LoseControll();
        yield return new WaitForSeconds(0.05f);
        yield return new WaitUntil(isNotAttacking);
        movement.GetControll();
    }

   private bool isNotAttacking()
    {
        return !playerData.animator.GetCurrentAnimatorStateInfo(playerData.animator.GetLayerIndex("Base Layer")).IsName("Attack");
   }
}
