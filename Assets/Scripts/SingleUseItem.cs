using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class SingleUseItem : UsableItem
{
    //[SerializeField] private GameObject prefabToGive;

    protected override void onItemPick(GameObject obj)
    {
        Debug.Log("picked item"+this.name);
       GameObject sinjgleUseItem= Instantiate(this.gameObject, obj.transform, false);
       sinjgleUseItem.active = false;
        obj.GetComponent<PlayerActionsController>().setSingleUseItem(sinjgleUseItem);
    }
}
