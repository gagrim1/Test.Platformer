using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public GameObject prefab;
    public Vector3 spawnPosition;
    public Animator animator;
    public Rigidbody2D rigidBody;
    public BoxCollider2D boxCollider;
    public float maxHealthPoints;
    public float healthPoints;
    public int maxDashCount;
    public int dashCount;
    public int maxJumpCount;
    public int jumpCount;
    public float moveSpeed;
    public float jumpSpeed;
    public float dashSpeed;
    public float dashTime;
    public bool isControlled;
    public bool isGrounded;
    public float pushSpeed;
    public float scale;
}