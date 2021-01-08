using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.GetComponent<ScoreManager>().GetCoin();
        Destroy(gameObject);
    }
}
