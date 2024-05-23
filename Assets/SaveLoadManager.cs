using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using JSONConverters;
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
public class GameData
{
    public Dictionary<Vector2Int, DungeonRoomSerializable> rooms;
    public Vector2Int currentRoom;
}
public class SaveLoadManager : MonoBehaviour
{
    private static SaveLoadManager _instance;

    public static SaveLoadManager instance
    {
        get => _instance;
    }
    public bool GameLoaded;

    private void Awake()
    {
        if (_instance != null)
            throw new Exception("there should be only one instance of Save Load Manager");
        _instance = this;
    }
    private string saveFilePath;

    void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
        Debug.Log(saveFilePath);
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

        //current Room
        data.currentRoom = DungeonGenerator.instance.getCurrRoom().pos;



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
                DungeonRoom room = new DungeonRoom(dungeonRoomSerializable.pos, dungeonRoomSerializable.roomType);
                rooms.Add(room.pos, room);
            }
            return rooms;
        }
        return null;
    }
}
