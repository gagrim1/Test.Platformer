﻿using System.Collections;
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
    public int maxHealthPoints;
    public int maxDashCount;
    public int maxJumpCount;
    public int dashCount;
    public int jumpCount;
    public float moveSpeed;
    public float jumpSpeed;
    public float dashSpeed;

}