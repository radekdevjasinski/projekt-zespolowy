using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PicableItem
{
    protected override void onItemPick(GameObject obj)
    {
        obj.GetComponent<PlayerItemsController>().addCoins(1);
    }
}
