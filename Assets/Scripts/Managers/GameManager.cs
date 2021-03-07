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
        AddFirstCheckpoint();
        CreatePlayer();
        Debug.Log("GameManager Awake was called!");
    }

    void CreatePlayer()
    {
        player = Instantiate(gameData.playerData.prefab, gameData.levelData.checkpoints[0].transform.position, Quaternion.identity, transform);
        gameData.playerData.animator = player.GetComponent<Animator>();
        gameData.playerData.rigidBody = player.GetComponent<Rigidbody2D>();
        gameData.playerData.boxCollider = player.GetComponent<BoxCollider2D>();
        gameData.playerData.soundManager = player.GetComponentInChildren<SoundManager>();
    }

    void OnApplicationQuit()
    {
        CleanCheckpoints();
    }

    void AddFirstCheckpoint()
    {
        if (gameData.levelData.checkpoints.Count == 0)
        {
            gameData.levelData.checkpoints.Add(firstCheckpoint);
        }
        else
        {
            gameData.levelData.checkpoints[0] = firstCheckpoint;
        }
    }

    void CleanCheckpoints()
    {
        //GameObject firstCheckpoint = gameData.levelData.checkpoints[0];
        gameData.levelData.checkpoints.Clear();
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
