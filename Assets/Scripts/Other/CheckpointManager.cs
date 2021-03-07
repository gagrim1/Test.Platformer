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
        Debug.Log("Checkpoints count: " + gameData.levelData.checkpoints.Count.ToString());
    }

    public void AddCheckpoint(GameObject checkpoint) {
        if(!gameData.levelData.checkpoints.Contains(checkpoint))
        {
            try
            {
                for (int i = 1; i <= gameData.levelData.checkpoints.Count; i++)
                {
                    if (gameData.levelData.checkpoints[i] == null)
                    {
                        gameData.levelData.checkpoints[i] = checkpoint;
                        break;
                    }
                }
                
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                Debug.Log("Exception: " + e);
                gameData.levelData.checkpoints.Add(checkpoint);
            }
        }
    }
}
