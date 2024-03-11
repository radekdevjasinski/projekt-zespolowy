using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform followPoint;
    // Update is called once per frame
    void Update()
    {
        transform.position=followPoint.position;
        //transform.position = Vector3.MoveTowards(transform.position, followPoint.position, timeChange);
    }
}
