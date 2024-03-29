using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PicableItem : EnterableObjects
{

    [SerializeField] private GameObject onItemPickSound;
  protected abstract void onItemPick(GameObject obj);

    protected override void onEnter(GameObject gameobject)
    {
        if (gameobject.CompareTag("Player"))
        {
            Debug.Log("enter");
            onItemPick(gameobject);
            if(onItemPickSound!=null)
                SoundManager.instance.playSound(onItemPickSound,this.transform.position);
            Destroy(this.gameObject);
        }
    }

    protected override void onExitEnter(GameObject gameobject)
    {
        //doNothign
    }
}
