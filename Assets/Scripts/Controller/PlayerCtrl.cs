using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    void Start()
    {
        GameController.Instance.player = GameController.Instance.database.playerData.player;
        Instantiate(GameController.Instance.player, new Vector3(0, 0, 29), Quaternion.identity);
    }
}
