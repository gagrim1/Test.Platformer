using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    public GameObject player;

    void Start()
    {
        CreatePlayer();
    }

    void CreatePlayer()
    {
        player = Instantiate(gameData.playerData.prefab, gameData.playerData.spawnPosition, Quaternion.identity, transform);
    }

}
