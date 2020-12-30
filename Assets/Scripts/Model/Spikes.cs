using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private int dashCount;
    private int maxDashCount;
    private bool inDash = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + " : " + "got damage from a trap" + " : " + Time.deltaTime);
        GameController.Instance.playerHealthSystem.Damage(10);
    }

}
