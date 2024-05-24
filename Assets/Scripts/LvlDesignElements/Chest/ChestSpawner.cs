using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChestSpawner : MonoBehaviour
{
    [Header("chests")]
    [SerializeField] private GameObject smallChest;
    [SerializeField] private GameObject bigChest;

    [Header("use Items")]
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject potion;

    [Header("Items")]
    [SerializeField] private List<GameObject> items = new List<GameObject>();

    [Header("random Chance")]
    [SerializeField] private static int spawnChestChance=10;
    [SerializeField] private static int minSpawnChestChance=10;
    [SerializeField] private static int maxSpawnChestChance=70;
    [SerializeField] private int bigChestChance;
    [SerializeField] private int minItemsAmountChest;
    [SerializeField] private int maxItemsAmountChest;

    [SerializeField] private bool ensureSpawn = false;

    private GameObject newCreatedChest;
    private void Start()
    {
        spawnChestChance = minSpawnChestChance;
    }
    public void SpawnChest()
    {
        //Debug.Log("-------------Spawing chest");
     int randomNumber=0;

         randomNumber = Random.Range(0, 101);
         if (randomNumber > spawnChestChance && !ensureSpawn)
         {
             if (spawnChestChance < maxSpawnChestChance)
             {
                 spawnChestChance += 10;
                 //Debug.Log("Enaching hcanes to " + spawnChestChance);
             }

             //Debug.Log("no chest usmmon: " + randomNumber + "> " + spawnChestChance +" == "+ensureSpawn);
         }
         else
         {
             spawnChestChance = minSpawnChestChance;
             randomNumber = Random.Range(0, 101);

             if (randomNumber > bigChestChance)
             {
                 CreateSmallChest();
             }
             else
             {
                 CreateBigChest();
             }
         }
     
    }



    private GameObject SellectNewItem()
    {
        int randomNumber = Random.Range(0, 4);
        switch(randomNumber)
        {
            case 0:
                return coin;
            case 1:
                return bomb;
            case 2:
                return key;
            default:
                return potion;
        }
    }
    private void CreateSmallChest()
    {
        Debug.Log("create small chest");
        newCreatedChest = Instantiate(smallChest, transform.position, Quaternion.identity, transform);
        int randomAmountOfItems = Random.Range(minItemsAmountChest, maxItemsAmountChest+1);
        LootChest lootChest = newCreatedChest.GetComponent<LootChest>();
        lootChest.loot = new List<GameObject>();
        for(int i = 0; i < randomAmountOfItems; i++)
        {
            lootChest.loot.Add(SellectNewItem());
        }
    }
    private void CreateBigChest()
    {
        //Debug.Log("create Big chest");
        newCreatedChest = Instantiate(bigChest, transform.position, Quaternion.identity, transform);
        int randomAmountOfItems = Random.Range(minItemsAmountChest, maxItemsAmountChest + 5);
        LootChest lootChest = newCreatedChest.GetComponent<LootChest>();
        lootChest.loot = new List<GameObject>();
        for (int i = 0; i < randomAmountOfItems; i++)
        {
            lootChest.loot.Add(SellectNewItem());
        }
        GameObject randomItem = items[Random.Range(0, items.Count)];
        if (randomItem != null)
        {
            lootChest.loot.Add(randomItem);
        }

    }
}
