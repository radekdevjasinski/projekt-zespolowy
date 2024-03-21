using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Update()
    {
        Vector3 moveTo = playerTransform.position;
        this.transform.position = new Vector3(moveTo.x, moveTo.y, -10);
    }
}
