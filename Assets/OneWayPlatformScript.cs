using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformScript : MonoBehaviour
{
    private PlatformEffector2D platform;
    [SerializeField]
    private LayerMask playerLayer;
    private void Start()
    {
        platform = GetComponent<PlatformEffector2D>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(jumpDown());
        }
    }

    IEnumerator jumpDown() {
        platform.colliderMask = 823;
        yield return new WaitForSeconds(0.15f);
        platform.colliderMask = -1;
    }
}
