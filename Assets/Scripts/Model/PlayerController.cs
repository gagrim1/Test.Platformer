using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Database database;
    float scale;
    bool inDash;
    Animator animator;

    private void Awake()
    {
        scale = database.playerData.scale;
        inDash = database.playerData.inDash;
        animator = database.playerData.animator;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovementController();
    }

    public void MovementController()
    {
        float moveSpeed = 20f;
        if (!inDash)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                database.playerData.playerRB.velocity = new Vector2(-moveSpeed, database.playerData.playerRB.velocity.y);
                animator.SetInteger("AnimState", 2);
            }
            else
            {
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    database.playerData.playerRB.velocity = new Vector2(+moveSpeed, database.playerData.playerRB.velocity.y);
                    animator.SetInteger("AnimState", 2);
                }
                else
                {
                    database.playerData.playerRB.velocity = new Vector2(0, database.playerData.playerRB.velocity.y);
                    animator.SetInteger("AnimState", 0);
                }
            }
        }
    }
}
