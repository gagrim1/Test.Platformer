using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float damageValue;

    private void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.GetComponent<HealthManager>().Damage(damageValue);
        col.gameObject.GetComponent<MovementManager>().DamagedPush(transform.position);
    }
}
