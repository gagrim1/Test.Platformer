using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public GameObject prefab;
    public GameObject spawnPoint;
    public Animator animator;
    public Rigidbody2D rigidBody;
    public BoxCollider2D boxCollider;
    public SoundManager soundManager;
    public float maxHealthPoints;
    public float healthPoints;
    public int coinScore;
    public int maxDashCount;
    public int dashCount;
    public int maxJumpCount;
    public int jumpCount;
    public float moveSpeed;
    public float jumpSpeed;
    public float dashSpeed;
    public float dashTime;
    public bool isAlive;
    public bool isControlled;
    public bool isGrounded;
    public bool isOnWall;
    public float pushSpeed;
    public float scale;
    public float maxFallVelocityUsual;
    public float maxFallVelocity;
    public float maxFallVelocityOnWall;
}