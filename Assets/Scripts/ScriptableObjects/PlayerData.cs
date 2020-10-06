using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject")]
public class PlayerData : ScriptableObject
{
    public GameObject player;

    public HealthSystem healthSystem;

    public Animator animator;

    public MovementScript move;

    public LayerMask platformMask;
    public LayerMask wallMask;

    public string prevWall = "";
    public Rigidbody2D playerRB;
    public BoxCollider2D playerBC;

    public int jumpCount;
    public int dashCount;
    public int maxJumpCount;
    public int maxDashCount;
    [SerializeField]
    public float scale = 2;
    public bool inDash = false;

}
