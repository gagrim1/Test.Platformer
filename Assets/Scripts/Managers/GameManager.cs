using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    public GameObject player;
    public GameObject spawnPoint;

    void Awake()
    {
        CreatePlayer();
    }

    void CreatePlayer()
    {
        player = Instantiate(gameData.playerData.prefab, spawnPoint.transform.position, Quaternion.identity, transform);
        gameData.playerData.spawnPoint = spawnPoint;
        gameData.playerData.animator = player.GetComponent<Animator>();
        gameData.playerData.rigidBody = player.GetComponent<Rigidbody2D>();
        gameData.playerData.boxCollider = player.GetComponent<BoxCollider2D>();
    }

}
