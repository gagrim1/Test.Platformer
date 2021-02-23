using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHeathManager : MonoBehaviour, IHealthManager
{
    // Start is called before the first frame update
    public float maxHealth = 100;
    private float health;
    public Animator animator;
    public bool isControlled;
    public bool isAlive=true;

    void Start()
    {
        health = maxHealth;
    }
    public void Heal(float healValue)
    {
        ChangeHealth(healValue);
    }

    public void Damage(float damageValue)
    {
        if (!isAlive)
            return;
        ChangeHealth(-damageValue);
        animator.SetTrigger("Hurt");
        //Debug.Log("Damage: " + damageValue.ToString());
        if (health == 0)
            StartCoroutine(Death());
    }

    public void ChangeHealth(float deltaHealth)
    {
        health = Mathf.Clamp(health + deltaHealth, 0.0f, maxHealth);
    }

    public IEnumerator Death()
    {
        isAlive = false;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitUntil(isNotHurt);
        animator.SetTrigger("Death");
        StartCoroutine(Recover());
    }

    public IEnumerator Recover()
    {
        Heal(maxHealth);
        animator.SetTrigger("Recover");
        yield return new WaitForSeconds(0.05f);
        yield return new WaitUntil(isNotDeath);
        yield return new WaitForSeconds(0.05f);
        yield return new WaitUntil(isNotRecover);
        isAlive = true;
    }

    private bool isNotHurt()
    {
        //Debug.Log("Hurt: " + animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer")).IsName("Hurt"));
        return !animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer")).IsName("Hurt");
    }

    private bool isNotDeath()
    {
        //Debug.Log("Death: " + animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer")).IsName("Death"));
        return !animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer")).IsName("Death");
    }

    private bool isNotRecover()
    {
        //Debug.Log("Recover: "+animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer")).IsName("Recover"));
        return !animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer")).IsName("Recover");
    }

    public void Kill()
    {
        Damage(maxHealth);
    }
}
