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
        if (GameController.Instance.playerHealthSystem.GetHealthAmount() > 0)
         {
             GameController.Instance.playerRB.velocity = Vector2.up * 25f;
         }
        else
        {
            GameController.Instance.playerRB.velocity = new Vector2(0, GameController.Instance.playerRB.velocity.y);
        }
    }

}
