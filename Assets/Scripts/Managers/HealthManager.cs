using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    public PlayerData playerData;
    UnityEvent healthChangedEvent;
    UIManager _ui;

    void Start()
    {
        playerData.isAlive = true;
        _ui = GameObject.FindWithTag("LevelScene").GetComponent<UIManager>();
        playerData.healthPoints = playerData.maxHealthPoints;
        if (healthChangedEvent == null) healthChangedEvent = new UnityEvent();
        healthChangedEvent.AddListener(_ui.RedrawHealthBar);
    }

    void ChangeHealth(float deltaHealth)
    {
        playerData.healthPoints = Mathf.Clamp(playerData.healthPoints + deltaHealth, 0.0f, playerData.maxHealthPoints);
        if (healthChangedEvent != null)
        {
            healthChangedEvent.Invoke();
        }
    }

    public void Damage(float damageValue)
    {
        if (playerData.isAlive)
        {
            playerData.animator.SetTrigger("Hurt");
            ChangeHealth(-damageValue);
            if (playerData.healthPoints == 0)
                Death();
        }
    }

    public void Heal(float healValue)
    {
        if (playerData.healthPoints == 0 && healValue > 0)
            Recover();
        ChangeHealth(healValue);
    }

    public void Death()
    {
        playerData.animator.SetBool("Run", false);
        playerData.animator.SetTrigger("Death");
        playerData.rigidBody.velocity = new Vector2(0, playerData.rigidBody.velocity.y);
        playerData.isAlive = false;
        playerData.isControlled = false;
    }

    public void Recover()
    {
        playerData.animator.SetTrigger("Recover");
        playerData.isAlive = true;
        playerData.isControlled = true;
    }
}
