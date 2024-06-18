using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZomieRotaitonSCript : MonoBehaviour
{


    void Start()
    {
        if (transform.rotation != new Quaternion(0, 0, 0, 1))
        {
            transform.rotation = new Quaternion(0, 0, 0, 1);
            Debug.Log("Fxinng "+ gameObject.GetInstanceID()+" raotion");
        }
    }


}
