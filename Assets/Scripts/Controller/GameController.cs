using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Database database;
    public GameObject player;
    public HealthSystem playerHealthSystem;
    public Rigidbody2D playerRB;
    public BoxCollider2D playerBC;

    public LayerMask platformMask;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }
}
