using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject")]
public class PlayerData : ScriptableObject
{
    public GameObject player;

    public int playerHP;

}

