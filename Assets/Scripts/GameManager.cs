using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    public GameObject player;

    void Awake()
    {
        CreatePlayer();
    }

    void CreatePlayer()
    {
        player = Instantiate(gameData.playerData.prefab, gameData.playerData.spawnPosition, Quaternion.identity, transform);
        gameData.playerData.animator = player.GetComponent<Animator>();
        gameData.playerData.rigidBody = player.GetComponent<Rigidbody2D>();
        gameData.playerData.boxCollider = player.GetComponent<BoxCollider2D>();
        gameData.playerData.isInDash = false;
        gameData.playerData.healthPoints = gameData.playerData.maxHealthPoints;
    }

}
