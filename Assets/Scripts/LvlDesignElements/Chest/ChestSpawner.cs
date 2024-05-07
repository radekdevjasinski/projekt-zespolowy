using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [Header("chests")]
    [SerializeField] private GameObject smallChest;
    [SerializeField] private GameObject bigChest;

    [Header("use Items")]
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject potion;

    [Header("normal Items")]
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
                //CreateBigChest()
            }
        }
    }

    private Vector2 CreatePosition()
    {
        return new Vector2(player.transform.position.x,player.transform.position.y);
        //Renderer renderer = currentRoom.GetComponent<Renderer>();
        //return new Vector2(renderer.bounds.size.x/2, renderer.bounds.size.y/2);
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
        Debug.Log("chest");
        newCreatedChest = Instantiate(smallChest, CreatePosition(), Quaternion.identity, transform);
        int randomAmountOfItems = Random.Range(minItemsAmountChest, maxItemsAmountChest+1);
        LootChest lootChest = newCreatedChest.GetComponent<LootChest>();
        lootChest.loot = new GameObject[randomAmountOfItems];
        for(int i = 0; i < randomAmountOfItems; i++)
        {
            lootChest.loot[i] = SellectNewItem();
        }
    }
    private void CreateBigChest()
    {
        newCreatedChest = Instantiate(bigChest, CreatePosition(), Quaternion.identity);
    }
}
