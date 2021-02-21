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
        if (!playerData.isControlled)
            return;
        //play animation
        movement.StopMoving();
        movement.LoseControll();
        playerData.animator.SetBool("Run",false);
        playerData.animator.SetTrigger("Attack");
        playerData.soundManager.PlayAttack();
        StartCoroutine(StayInAttack());
        StartCoroutine(DelayedAttack());
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator StayInAttack()
    {
        yield return new WaitForSeconds(0.05f);
        yield return new WaitUntil(IsNotAttacking);
        movement.GetControll();
    }

    IEnumerator DelayedAttack()
    {
        yield return new WaitForSeconds(0.2f);
        //check enemys
        enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //hurt enemys
        foreach (Collider2D enemy in enemies)
        {
            //Debug.Log(enemy.name);
            enemy.GetComponent<IHealthManager>().Damage(10);
        }
    }

    private bool IsNotAttacking()
    {
        return !playerData.animator.GetCurrentAnimatorStateInfo(playerData.animator.GetLayerIndex("Base Layer")).IsName("Attack");
   }
}
