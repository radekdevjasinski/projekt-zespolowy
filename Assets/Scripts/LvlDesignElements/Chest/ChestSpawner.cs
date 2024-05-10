using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private int spawnChestChance;
    [SerializeField] private int minSpawnChestChance;
    [SerializeField] private int maxSpawnChestChance;
    [SerializeField] private int bigChestChance;
    [SerializeField] private int minItemsAmountChest;
    [SerializeField] private int maxItemsAmountChest;

    private GameObject newCreatedChest;
    private GameObject currentRoom;
    private void Start()
    {
        spawnChestChance = minSpawnChestChance;
    }
    public void SpawnChest(GameObject currentRoom)
    {
        this.currentRoom = currentRoom;
        int randomNumber=0;

        randomNumber = Random.Range(0, 101);
        if(randomNumber > spawnChestChance)
        {
            if (spawnChestChance < maxSpawnChestChance)
            {
                spawnChestChance += 10;
            }
        }
        else
        {
            spawnChestChance = minSpawnChestChance;
            randomNumber = Random.Range(0, 101);

            if(randomNumber > bigChestChance)
            {
                CreateSmallChest();
            }
            else
            {
                CreateBigChest();
            }
        }
    }

    private Vector2 CreatePosition()
    {
        TilemapRenderer renderer = currentRoom.transform.GetChild(0).GetComponent<TilemapRenderer>();
        return new Vector2(currentRoom.transform.position.x + (renderer.bounds.size.x/2), currentRoom.transform.position.y + (renderer.bounds.size.y/2));
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
        newCreatedChest = Instantiate(smallChest, CreatePosition(), Quaternion.identity, transform);
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
        newCreatedChest = Instantiate(bigChest, CreatePosition(), Quaternion.identity, transform);
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