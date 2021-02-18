using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthManager : MonoBehaviour, IHealthManager
{
    public PlayerData playerData;
    public MovementManager _move;
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

    public void ChangeHealth(float deltaHealth)
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
            _move.DamagedPush(new Vector3(transform.localScale.x,0,0));
            ChangeHealth(-damageValue);
            if (playerData.healthPoints == 0)
                StartCoroutine(Death());
        }
    }

    public void Heal(float healValue)
    {
        ChangeHealth(healValue); 
    }

    public IEnumerator Death()
    {
        playerData.soundManager.playDeath();
        playerData.isAlive = false;
        _move.loseControllEvent.Invoke();
        playerData.animator.SetBool("Run", false);
        yield return new WaitForSeconds(0.5f);
        playerData.animator.SetTrigger("Death");
        playerData.rigidBody.velocity = new Vector2(0, playerData.rigidBody.velocity.y);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(Recover());
    }

    public IEnumerator Recover()
    {
        playerData.soundManager.playRecover();
        Heal(playerData.maxHealthPoints);
        _move.Respawn();
        playerData.animator.SetTrigger("Recover");
        yield return new WaitForSeconds(1.6f);
        playerData.isAlive = true;
        _move.getControllEvent.Invoke();
    }
}
