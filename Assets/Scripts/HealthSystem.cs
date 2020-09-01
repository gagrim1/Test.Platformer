using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler onDamage;
    public event EventHandler onHeal;
    private int healthAmount;
    private int healthAmountMax;

    public HealthSystem(int healthAmount)
    {
        healthAmountMax = healthAmount;
        this.healthAmount = healthAmount;
    }

    public void Damage(int amount)
    {
        healthAmount -= amount;
        if (healthAmount < 0) {
            healthAmount = 0;
        }
        if(onDamage != null) onDamage(this, EventArgs.Empty);
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
