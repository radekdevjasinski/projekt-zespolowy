using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroundAffect : EnterableObjects
{
    protected override void onEnter(GameObject entity)
    {
        Debug.Log("Enter gorudn");
        EntityController entityController;
        if (entity.TryGetComponent<EntityController>(out entityController))
        {
            onEnterGround(entityController);
        }
    }

    protected override void onExitEnter(GameObject entity)
    {
        Debug.Log("exit gorudn");
        EntityController entityController;
        if (entity.TryGetComponent<EntityController>(out entityController))
        {
            onExitGround(entityController);
        }
    }

   protected abstract void onEnterGround(EntityController entity);
   protected abstract void onExitGround(EntityController entity);
}
