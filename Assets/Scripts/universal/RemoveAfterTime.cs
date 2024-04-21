using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAfterTime : MonoBehaviour
{
    [SerializeField] private float timeToDelete;
    void Start()
    {
     Invoke("remove",timeToDelete);   
    }

   private void remove()
    {
        Destroy(this.gameObject);
    }
}
