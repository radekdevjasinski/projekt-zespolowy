using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemToPick : MonoBehaviour
{



    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        Destroy(this.gameObject);
    }



    public ActiveItemBase getActiveItemScript()
    {
        return GetComponent<ActiveItemBase>();
    }




    
}
