using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCameraOnStart : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Shake CAmera");
        CameraController.Instance.Shake();
        ;
    }


}
