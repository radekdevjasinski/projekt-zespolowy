using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectUponDeath : MonoBehaviour,DeathSeuance
{

    [SerializeField] GameObject paritcleObject;

  
    public void onDeath()
    {
        Debug.Log("spawn object upon death, in "+ transform.parent.name);
        Instantiate(paritcleObject,this.transform.position,this.transform.rotation,transform.parent);
    }
}
