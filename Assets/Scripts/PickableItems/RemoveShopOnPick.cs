using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveShopOnPick : MonoBehaviour,IonItemPickUP
{
    public void onItemPicked(GameObject gameobject)
    {
        GetComponentInParent<NpcTrader>().removeShop();
    }
}
