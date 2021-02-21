using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    public float damageValue;
    public float phaseTime;
    public Animator controllerIsUp;
    public bool isMoving;
    public bool isMovingOnTrigger;
    public float timeBeforeTrigger;
    public float timeAfterTrigger;
    public bool isUp = true;
    private List<Collider2D> targets = new List<Collider2D>();
    void Start()
    {
        if (isMoving && !isMovingOnTrigger)
        {
            StartCoroutine(ChangeState());
        }
        controllerIsUp.SetBool("isUp", isUp);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isUp)
        {
            Damage(col);
        }
        else
        {
            if (isMovingOnTrigger)
                StartCoroutine(DelayedChangeState(timeBeforeTrigger, timeAfterTrigger));
            targets.Add(col);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(targets.Contains(collision))
            targets.Remove(collision);
    }
    IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(phaseTime);
        controllerIsUp.SetBool("isUp", !isUp);
        yield return new WaitForSeconds(0.2f);
        isUp = !isUp;
        if (isUp)
        {
            targets.ForEach(Damage);
        }
        StartCoroutine(ChangeState());
    }

    IEnumerator DelayedChangeState(float firstDealay, float secondDelay)
    {
        yield return new WaitForSeconds(firstDealay);
        controllerIsUp.SetBool("isUp", true);
        isUp = true;
        if (isUp)
        {
            targets.ForEach(Damage);
        }
        yield return new WaitForSeconds(secondDelay);
        controllerIsUp.SetBool("isUp", false);
        yield return new WaitForSeconds(0.2f);
        isUp = false;
    }
    private void Damage(Collider2D col)
    {
        col.gameObject.GetComponent<IHealthManager>().Damage(damageValue);
    }
}
