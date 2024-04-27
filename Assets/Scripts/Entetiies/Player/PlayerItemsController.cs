using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsController : MonoBehaviour
{
    [SerializeField]
    private int coins = 0;
    [SerializeField]
    private int keys = 0;
    [SerializeField]
    private int heathPotions = 0;
    [SerializeField]
    private int bombs = 0;

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

    internal void addKey(int keys)
    {
        this.keys += keys;
    }
    internal void removeKey(int keys)
    {
        this.keys -= keys;
    }
    internal int getKeys()
    {
        return this.keys;
    }

    internal void addHelathPotion(int potions)
    {
        heathPotions += potions;
    }
    internal void removeHelathPotion(int potions)
    {
        heathPotions -= potions;
    }
    internal int getHelathPotion()
    {
        return heathPotions;
    }

    internal void addBomb(int bomb)
    {
        this.bombs += bomb;
    }
    internal void removeBombs(int bomb)
    {
        this.bombs -= bomb;
    }
    internal int getBombs()
    {
        return this.bombs;
    }

 
}
