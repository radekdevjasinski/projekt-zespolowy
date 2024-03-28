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
            Debug.Log("enter");
            onItemPick(gameobject);
            Destroy(this.gameObject);
        }
    }

    protected override void onExitEnter(GameObject gameobject)
    {
        //doNothign
    }
}
