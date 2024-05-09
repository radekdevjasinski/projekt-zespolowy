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
        EntityController<float> entityController;
        if (obj.TryGetComponent<EntityController<float>>(out entityController))
        {
            entityController.dealDamage(this.baseDamage*damageModifer);
        }
        //throw new System.NotImplementedException();
    }


}
