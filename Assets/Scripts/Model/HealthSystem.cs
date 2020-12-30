using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler onDamage;
    public event EventHandler onHeal;
    private bool canDamage=true;
    [SerializeField]
    private int healthAmount=100;
    [SerializeField]
    private int healthAmountMax=100;

    public HealthSystem(int healthAmount)
    {
        healthAmountMax = healthAmount;
        this.healthAmount = healthAmount;
    }

    private void Start()
    {
        healthAmountMax = 100;
        healthAmount = healthAmountMax;
    }

    public void Damage(int amount)
    {
        if (canDamage)
        {
            healthAmount -= amount;
            if (healthAmount < 0)
            {
                healthAmount = 0;
            }

            if (GetHealthAmount() > 0)
            {
                GameController.Instance.playerRB.velocity = (Vector2.up + Vector2.right) * 25f;
            }
            else
            {
                GameController.Instance.playerRB.velocity = new Vector2(0, GameController.Instance.playerRB.velocity.y);
            }
            StartCoroutine(StayImmortal());
            if (onDamage != null) onDamage(this, EventArgs.Empty);
        }
    }
    IEnumerator StayImmortal()
    {
        if (canDamage == true)
        {
            canDamage = false;
            yield return new WaitForSeconds(1.0f);
            canDamage = true;
        }
    }

    public void Heal(int amount)
    {
        healthAmount += amount;
        if (healthAmount > healthAmountMax) {
            healthAmount = healthAmountMax;
        }
        if (onHeal != null) onHeal(this, EventArgs.Empty);
    }

    public float GetHealthNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }

    public int GetHealthAmount() 
    {
        return healthAmount;
    }
}
