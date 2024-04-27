using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PicableItem : EnterableObjects
{

  protected abstract void onItemPick(GameObject obj);

    protected override void onEnter(GameObject gameobject)
    {
        if (gameobject.CompareTag("Player"))
        {
            if (vaildatePickingIp(gameobject))
            {
                onItemPick(gameobject);
                onItemPickUp(gameobject);
               Destroy(this.gameObject);
            }
        }
    }

    private bool vaildatePickingIp(GameObject gameobject)
    {
        IitemPickUpValidaitin[] validations = GetComponents<IitemPickUpValidaitin>();
        foreach (IitemPickUpValidaitin valid in validations)
        {
            if(!valid.canBePicked(gameobject))
                return false;
        }
        return true;
    }

    private void onItemPickUp(GameObject gameobject)
    {
        Debug.Log("on item pick");
        IonItemPickUP[] picks = GetComponents<IonItemPickUP>();
        foreach (IonItemPickUP pick in picks)
        {
        pick.onItemPicked(gameobject);
        }

    }

    protected override void onExitEnter(GameObject gameobject)
    {
        //doNothign
    }
}
