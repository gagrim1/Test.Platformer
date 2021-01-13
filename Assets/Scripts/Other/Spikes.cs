using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float damageValue;
    public bool isUp=true;
    public Animator controllerIsUp;

    private void Start()
    {
        StartCoroutine(ChangeState());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isUp)
        {
            col.gameObject.GetComponent<HealthManager>().Damage(damageValue);
            col.gameObject.GetComponent<MovementManager>().DamagedPush(transform.position);
        }

    }
    IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(3f);
        isUp = !isUp;
        controllerIsUp.SetBool("isUp", isUp);
        StartCoroutine(ChangeState());
    }
}
