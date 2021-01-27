using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameData gameData;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag != "Player")
        {
            return ;
        }
        AddCheckpoint(gameObject);
    }

    public void AddCheckpoint(GameObject checkpoint){
        if(!gameData.levelData.checkpoints.Contains(checkpoint)){
            gameData.levelData.checkpoints.Add(checkpoint);
        }
    }
}
