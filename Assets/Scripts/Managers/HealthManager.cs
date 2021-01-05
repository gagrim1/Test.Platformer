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
        ChangeHealth(-damageValue);
    }

    public void Heal(float healValue)
    {
        ChangeHealth(healValue);
    }
}
