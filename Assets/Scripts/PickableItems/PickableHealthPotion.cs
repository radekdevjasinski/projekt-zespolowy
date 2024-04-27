using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PickableHealthPotion : PicableItem
{
    protected override void onItemPick(GameObject obj)
    {
        obj.GetComponent<PlayerItemsController>().addHelathPotion(1);
    }


}
