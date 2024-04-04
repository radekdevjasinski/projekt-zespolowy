using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private DestroyableObject destroyableObject;

    private void Awake()
    {
        destroyableObject = GetComponent<DestroyableObject>();
    }
    private void FixedUpdate()
    {
        if (destroyableObject.health <= 0)
        {
            DestroyThisObject();
        }
    }

    private void DestroyThisObject()
    {
        Destroy(this.gameObject);
    }
}
