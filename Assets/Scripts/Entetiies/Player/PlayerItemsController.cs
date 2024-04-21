using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsController : MonoBehaviour
{
    private int coins = 0;

    public void addCoins(int coins)
    {
        this.coins+= coins;
    }

    public void remvoveCoins(int coins)
    {
        this.coins -= coins;
    }

    public int getCoinsAmount()
    {
        return coins;
    }

}
