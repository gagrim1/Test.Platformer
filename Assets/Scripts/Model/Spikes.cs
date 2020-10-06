using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponent<HealthBar>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            // Vector2 knockBackDir = (player.GetPosition() - transform.position).normalized;
            // player.DamageKnockBack(knockBackDir, 10f);
            player.GetDamage(10);
            healthBar.GetDamage(10);
        }
    }
}
