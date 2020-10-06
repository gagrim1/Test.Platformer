using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField]
    private TestScriptableObject testScriptableObject;
    [SerializeField]
    private Database database;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(testScriptableObject.myString);
        Debug.Log(database.ToString());
        Debug.Log(database.playerData.ToString());
        Debug.Log(database.playerData.scale.ToString());
    }
}
