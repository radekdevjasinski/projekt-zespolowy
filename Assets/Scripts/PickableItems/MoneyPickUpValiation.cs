using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickUpValiation : MonoBehaviour, IitemPickUpValidaitin
{
    private int moneyRequired = 0;

    public void setMoneyRequired(int moneyRequired)
    {  this.moneyRequired = moneyRequired; }

    public bool canBePicked(GameObject pickingUpActor)
    {
        PlayerItemsController playerItemsController = pickingUpActor.GetComponent<PlayerItemsController>();
        if (playerItemsController != null)
        {
            return playerItemsController.getCoinsAmount() >= moneyRequired;
        }
        else
        {
            throw new System.Exception("nor player item contoller");
        }
        return false;
    }
}
