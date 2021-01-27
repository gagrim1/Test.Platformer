using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    public GameObject player;
    public GameObject firstCheckpoint;

    void Awake()
    {
        CreatePlayer();
    }

    void CreatePlayer()
    {
        AddFirstCheckpoint();
        player = Instantiate(gameData.playerData.prefab, gameData.levelData.checkpoints[0].transform.position, Quaternion.identity, transform);
        gameData.playerData.animator = player.GetComponent<Animator>();
        gameData.playerData.rigidBody = player.GetComponent<Rigidbody2D>();
        gameData.playerData.boxCollider = player.GetComponent<BoxCollider2D>();
    }

    void OnApplicationQuit()
    {
        CleanCheckpoints();
    }

    void AddFirstCheckpoint()
    {
        gameData.levelData.checkpoints.Add(firstCheckpoint);
    }

    void CleanCheckpoints()
    {
        //GameObject firstCheckpoint = gameData.levelData.checkpoints[0];
        gameData.levelData.checkpoints.Clear();
    }
}
