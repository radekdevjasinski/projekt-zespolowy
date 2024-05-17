using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsController : MonoBehaviour
{
    [SerializeField] 
    private ItemUIPanel itemUIPanel;
    [SerializeField]
    private int coins = 0;
    [SerializeField]
    private int keys = 0;
    [SerializeField]
    private int heathPotions = 0;
    [SerializeField]
    private int bombs = 0;
    [SerializeField]
    private bool _hasGoldenCoin = false;

    private void Start()
    {
        itemUIPanel.SetValuesToText(coins, bombs, keys, heathPotions);
    }
    public void addCoins(int coins)
    {
        this.coins+= coins;
        itemUIPanel.SetValuesToText(this.coins, bombs, keys, heathPotions);
    }

    public void remvoveCoins(int coins)
    {
        this.coins -= coins;
        itemUIPanel.SetValuesToText(this.coins, bombs, keys, heathPotions);
    }

    public int getCoinsAmount()
    {
        return coins;
    }

    internal void addKey(int keys)
    {
        this.keys += keys;
        itemUIPanel.SetValuesToText(coins, bombs, this.keys, heathPotions);
    }
    internal void removeKey(int keys)
    {
        this.keys -= keys;
        itemUIPanel.SetValuesToText(coins, bombs, this.keys, heathPotions);
    }
    internal int getKeys()
    {
        return this.keys;
    }

    internal void addHelathPotion(int potions)
    {
        heathPotions += potions;
        itemUIPanel.SetValuesToText(coins, bombs, keys, this.heathPotions);
    }
    internal void removeHelathPotion(int potions)
    {
        heathPotions -= potions;
        itemUIPanel.SetValuesToText(coins, bombs, keys, this.heathPotions);
    }
    internal int getHelathPotion()
    {
        return heathPotions;
    }

    internal void addBomb(int bomb)
    {
        this.bombs += bomb;
        itemUIPanel.SetValuesToText(coins, this.bombs, keys, heathPotions);
    }
    internal void removeBombs(int bomb)
    {
        this.bombs -= bomb;
        itemUIPanel.SetValuesToText(coins, this.bombs, keys, heathPotions);
    }
    internal int getBombs()
    {
        return this.bombs;
    }

    internal void addGoldCoin()
    {
        _hasGoldenCoin = true;
    }

    internal bool hasGoldenCoin()
    {
        return _hasGoldenCoin;
    }

    internal void removeGoldCoin()
    {
        _hasGoldenCoin=false;
    }
}
