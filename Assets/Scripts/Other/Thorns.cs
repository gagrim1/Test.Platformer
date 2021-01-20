using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    public float damageValue;
    public float phaseTime;
    public Animator controllerIsUp;
    public bool isMoving;
    private bool isUp = true;
    private List<Collider2D> targets = new List<Collider2D>();
    void Start()
    {
        if(isMoving)
        {
            StartCoroutine(ChangeState());
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isUp)
        {
            Damage(col);
        }
        else
            targets.Add(col);
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
    private void Damage(Collider2D col)
    {
        col.gameObject.GetComponent<HealthManager>().Damage(damageValue);
        col.gameObject.GetComponent<MovementManager>().DamagedPush(transform.position);
    }
}
