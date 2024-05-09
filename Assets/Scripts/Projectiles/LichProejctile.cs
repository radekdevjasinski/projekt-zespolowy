using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichProejctile : Projectile
{

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected override void OnDestroyed()
    {
       // throw new System.NotImplementedException();
    }

    protected override void onHit(GameObject obj)
    {
        //Debug.Log("Lich proejcitle hit : " + obj.name);
        EntityController<int> entityController;
        if (obj.CompareTag("Player") && obj.TryGetComponent<EntityController<int>>(out entityController))
        {
            Vector2 damageDirection = -rb.velocity.normalized;
            Debug.Log("Lich proejcitle dweal dagmage: "+ obj.name);
            entityController.dealDamage((int)(this.baseDamage*damageModifer));

            //entityController.dealDamage((int)this.baseDamage, damageDirection);
        }
        //throw new System.NotImplementedException();
    }


}
