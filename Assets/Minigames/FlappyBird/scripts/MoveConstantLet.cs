using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveConstantLet : MonoBehaviour
{
    [SerializeField] private float speed;
    
    protected virtual void Update()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
    }
}
