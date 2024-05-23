using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using JSONConverters;
[System.Serializable]
public class GameData
{
    //dungeon
    public Dictionary<Vector2Int, DungeonRoomSerializable> rooms;
    public Vector2Int currentRoom;

    //player
    public PlayerDataSerializable player;

    //ui items
    public PlayerItemsSerializable items;
}
[System.Serializable]
public class DungeonRoomSerializable
{
    public Vector2Int pos;
    public RoomType roomType;
    public int enemiesCount;
    public bool visited;


    public DungeonRoomSerializable(Vector2Int pos, RoomType roomType, int enemiesCount ,bool visited)
    {
        this.pos = pos;
        this.roomType = roomType;
        this.enemiesCount = enemiesCount;
        this.visited = visited;
    }
}
[System.Serializable]
public class PlayerDataSerializable
{
    public int MaxHealth;
    public int health;
    public float MaxStamina;
    public float stamina;
    public int armor;
    public float speed;
    public float fireRate;
    public float damage;
    public float range;
    public PlayerDataSerializable(int maxHealth, int health, float maxStamina, float stamina, int armor, float speed, float fireRate, float damage, float range)
    {
        this.MaxHealth = maxHealth;
        this.health = health;
        this.MaxStamina = maxStamina;
        this.stamina = stamina;
        this.armor = armor;
        this.speed = speed;
        this.fireRate = fireRate;
        this.damage = damage;
        this.range = range;
    }
}
[System.Serializable]
public class PlayerItemsSerializable
{
    public int coins;
    public int keys;
    public int healthPotions;
    public int bombs;
    public PlayerItemsSerializable(int coins, int keys, int healthPotions, int bombs)
    {
        this.coins = coins;
        this.keys = keys;
        this.healthPotions = healthPotions;
        this.bombs = bombs;
    }
}

public class SaveLoadManager : MonoBehaviour
{
    private static SaveLoadManager _instance;
    private string saveFilePath;

    public static SaveLoadManager instance
    {
        get => _instance;
    }
    public bool GameLoaded;

    private void Awake()
    {
        if (_instance != null)
            throw new Exception("there should be only one instance of Save Load Manager");
        saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");

        _instance = this;

        if (PlayerPrefs.GetInt("Loaded") == 1)
        {
            GameLoaded = true;
        }
        
    }

    public void SaveGame()
    {
        GameData data = new();
        //dungeon
        Dictionary<Vector2Int, DungeonRoom> rooms = DungeonGenerator.instance.rooms;
        Dictionary<Vector2Int, DungeonRoomSerializable> roomsSerializable = new();
        foreach (DungeonRoom room in rooms.Values)
        {
            DungeonRoomSerializable roomSerializable = new(room.pos, room.roomType, room.enemiesCount, room.visited);
            roomsSerializable.Add(room.pos, roomSerializable);
        }
        data.rooms = roomsSerializable;


        //player stats
        PlayerAttributesController playerAttributesController = GameObject.Find("Player").GetComponent<PlayerAttributesController>();
        PlayerDataSerializable playerDataSerializable = new(
            playerAttributesController.getMaxHealth(),
            playerAttributesController.Health,
            playerAttributesController.getMaxStamina(),
            playerAttributesController.getMaxStamina(), //gets max stamina
            playerAttributesController.Armor,
            playerAttributesController.Speed,
            playerAttributesController.FireRate,
            playerAttributesController.Damage,
            playerAttributesController.Range
            );
        data.player = playerDataSerializable;

        //current Room
        data.currentRoom = DungeonGenerator.instance.getCurrRoom().pos;

        // items
        PlayerItemsController playerItemsController = GameObject.Find("Player").GetComponent<PlayerItemsController>();
        data.items = new(
            playerItemsController.getCoins(),
            playerItemsController.getKeys(),
            playerItemsController.getHealthPotion(),
            playerItemsController.getBombs());

        JsonSerializerSettings settings = new();
        settings.Converters.Add(new Vector2IntConverter());
        settings.Converters.Add(new DictionaryVector2IntKeyConverter());
        settings.Formatting = Formatting.Indented;
        string json = JsonConvert.SerializeObject(data, settings);
        File.WriteAllText(saveFilePath, json);
    }

    public GameData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            JsonSerializerSettings settings = new();
            settings.Converters.Add(new Vector2IntConverter());
            settings.Converters.Add(new DictionaryVector2IntKeyConverter());
            return JsonConvert.DeserializeObject<GameData>(json, settings);
        }
        return null;
    }
    public Dictionary<Vector2Int, DungeonRoom> TranslateRooms(GameData data)
    {
        if (data.rooms != null)
        {
            Dictionary<Vector2Int, DungeonRoomSerializable> roomsSerializable = data.rooms;
            Dictionary<Vector2Int, DungeonRoom> rooms = new();
            foreach(DungeonRoomSerializable dungeonRoomSerializable in roomsSerializable.Values)
            {
                DungeonRoom room = new DungeonRoom(dungeonRoomSerializable.pos, dungeonRoomSerializable.roomType, dungeonRoomSerializable.visited);
                rooms.Add(room.pos, room);
            }
            return rooms;
        }
        return null;
    }
}
