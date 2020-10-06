using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "ScriptableObjects/Database")]
public class Database : ScriptableObject
{
    public PlayerData playerData;
    public HealthSystem healthSystem;
}
