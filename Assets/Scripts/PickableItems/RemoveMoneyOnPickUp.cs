using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveMoneyOnPickUp : MonoBehaviour, IonItemPickUP
{
    private int moneyToRemove = 0;

    public void onItemPicked(GameObject gameobject)
    {
        gameobject.GetComponent<PlayerItemsController>().remvoveCoins(moneyToRemove);
    }

    public void setMoneyToRemove(int moneyToRemove) {
        this.moneyToRemove = moneyToRemove;
    }



}
