using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPick : PicableItem
{
    protected override void onItemPick(GameObject obj)
    {
        obj.GetComponent<PlayerItemsController>().addKey(1);
    }
}
