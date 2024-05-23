using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : PicableItem
{
    protected override void onItemPick(GameObject obj)
    {
        obj.GetComponent<PlayerItemsController>().addGoldCoin();
    }
}
