using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollsion : MonoBehaviour
{
    [SerializeField]
    private float dmgAmount;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        EntityController<int> entityController = collision.gameObject.GetComponent<EntityController<int>>();
        if(entityController == null )
        {
            EntityController<float> entityControllerf = collision.gameObject.GetComponent<EntityController<float>>();
            if( entityControllerf != null )
            {
                entityControllerf.dealDamage(dmgAmount);
            }
        }
        else
            entityController.dealDamage(Convert.ToInt32(dmgAmount));
    }

}
