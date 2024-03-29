using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : Projectile
{

    
    protected override void OnDestroyed()
    {
       // throw new System.NotImplementedException();
    }

    protected override void onHit(GameObject obj)
    {
        EntityController entityController;
        if (obj.TryGetComponent<EntityController>(out entityController))
        {
            entityController.dealDamage(this.baseDamage);
        }
        //throw new System.NotImplementedException();
    }


}
