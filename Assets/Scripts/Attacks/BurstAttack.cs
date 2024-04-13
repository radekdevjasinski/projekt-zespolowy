using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
  


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll == true)
        {
            Debug.Log("Collsion: "+ coll.transform.name);
            if (coll.gameObject.CompareTag("Player"))
            {
                coll.GetComponent<EntityController<int>>().dealDamage(1);
            }
        }
    }
}
