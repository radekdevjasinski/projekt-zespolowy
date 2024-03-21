using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    private Transform player;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 newPos = player.position;
        this.transform.position = new Vector3(newPos.x, newPos.y, -10f);
    }
}
