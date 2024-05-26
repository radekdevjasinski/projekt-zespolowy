using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform followPoint;


    void Update()
    {
        if (followPoint != null)
        {
            Vector3 moveTo = followPoint.position;
            this.transform.position = new Vector3(moveTo.x, moveTo.y, -10);
        }

    }

    public Transform getFollowPoint()
    {
        return followPoint;
    }

    public void setFollowPoint(Transform newfollowPoint)
    {
        this.followPoint = newfollowPoint;
    }
}
