using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickableBomb : PicableItem
{
    protected override void onItemPick(GameObject obj)
    {
        obj.GetComponent<PlayerItemsController>().addBomb(1);
    }
}
