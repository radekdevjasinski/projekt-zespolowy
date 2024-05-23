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
    private int healthPotions = 0;
    [SerializeField]
    private int bombs = 0;


    private void Start()
    {
        itemUIPanel.SetValuesToText(coins, bombs, keys, healthPotions);
    }
    public void addCoins(int coins)
    {
        this.coins+= coins;
        itemUIPanel.SetValuesToText(this.coins, bombs, keys, healthPotions);
    }

    public void removeCoins(int coins)
    {
        this.coins -= coins;
        itemUIPanel.SetValuesToText(this.coins, bombs, keys, healthPotions);
    }

    public int getCoins()
    {
        return coins;
    }

    internal void addKey(int keys)
    {
        this.keys += keys;
        itemUIPanel.SetValuesToText(coins, bombs, this.keys, healthPotions);
    }
    internal void removeKey(int keys)
    {
        this.keys -= keys;
        itemUIPanel.SetValuesToText(coins, bombs, this.keys, healthPotions);
    }
    internal int getKeys()
    {
        return this.keys;
    }

    internal void addHealthPotion(int potions)
    {
        healthPotions += potions;
        itemUIPanel.SetValuesToText(coins, bombs, keys, this.healthPotions);
    }
    internal void removeHealthPotion(int potions)
    {
        healthPotions -= potions;
        itemUIPanel.SetValuesToText(coins, bombs, keys, this.healthPotions);
    }
    internal int getHealthPotion()
    {
        return healthPotions;
    }

    internal void addBomb(int bomb)
    {
        this.bombs += bomb;
        itemUIPanel.SetValuesToText(coins, this.bombs, keys, healthPotions);
    }
    internal void removeBombs(int bomb)
    {
        this.bombs -= bomb;
        itemUIPanel.SetValuesToText(coins, this.bombs, keys, healthPotions);
    }
    internal int getBombs()
    {
        return this.bombs;
    }

    public void SetCoins(int coins)
    {
        this.coins = coins;
        itemUIPanel.SetValuesToText(this.coins, bombs, keys, healthPotions);
    }

    public void SetKeys(int keys)
    {
        this.keys = keys;
        itemUIPanel.SetValuesToText(coins, bombs, this.keys, healthPotions);
    }
    public void SetHealthPotions(int healthPotions)
    {
        this.healthPotions = healthPotions;
        itemUIPanel.SetValuesToText(coins, bombs, keys, this.healthPotions);
    }

    public void SetBombs(int bombs)
    {
        this.bombs = bombs;
        itemUIPanel.SetValuesToText(coins, this.bombs, keys, healthPotions);
    }
}
