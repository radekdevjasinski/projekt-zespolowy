using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum RoomType
{
    STARTROOM,
    ROOM,
    SHOPROOM,
    BOSSROOM
}
public class DungeonRoom
{
    public int id;
    public Vector2Int pos;
    public RoomType roomType;
    public GameObject gameObject;

    public DungeonRoom(int id, Vector2Int pos, RoomType roomType)
    {
        this.id = id;
        this.pos = pos;
        this.roomType = roomType;
    }

    public List<Vector2Int> freeSpots(List<DungeonRoom> rooms)
    {
        Vector2Int[] directions = { new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(-1, 0), new Vector2Int(0, -1) };
        List<Vector2Int> free = new List<Vector2Int>();
        for (int i = 0; i < directions.Length; i++)
        {
            bool taken = false;
            for (int j = 0; j < rooms.Count; j++)
            {
                if (this.pos + directions[i] == rooms[j].pos)
                {
                    taken = true;
                }
            }
            if (!taken)
            {
                free.Add(this.pos + directions[i]);
            }
        }
        return free;
    }
}
public class DungeonGenerator : MonoBehaviour
{
    public int roomSpace = 25;

    public int roomCount;
    public int shopRoomCount;
    public int bossRoomCount;

    public GameObject smallPrefab;

    public List<DungeonRoom> rooms = new List<DungeonRoom>();

    void Start()
    {
        //int room = roomCount + shopRoomCount + bossRoomCount;
        List<Vector2Int> freeRooms = new List<Vector2Int>();


        rooms.Add(new DungeonRoom(0, new Vector2Int(0, 0), RoomType.STARTROOM));
        rooms.Add(new DungeonRoom(1, new Vector2Int(0, 1), RoomType.ROOM));
        rooms.Add(new DungeonRoom(1, new Vector2Int(0, 2), RoomType.ROOM));
        rooms.Add(new DungeonRoom(1, new Vector2Int(0, 3), RoomType.ROOM));
        rooms.Add(new DungeonRoom(1, new Vector2Int(1, 1), RoomType.ROOM));
        rooms.Add(new DungeonRoom(1, new Vector2Int(2, 1), RoomType.ROOM));
        rooms.Add(new DungeonRoom(2, new Vector2Int(-1, 0), RoomType.ROOM));



        /*for (int i = 0; i < roomCount; i++)
        {
            freeRooms = CalculateFreeRooms();
            if (smallRoomCount > 0 || bigRoomCount > 0)
            {
                int random = Random.Range(0, 100);
                if ( > 50)
                {
                    rooms.Add(new DungeonRoom(roomCount, freeRooms[], RoomType.STARTROOM));

                }
            }
        }
        
        freeRooms.AddRange(rooms[1].freeSpots(rooms));*/
        DrawRooms();
    }
    List<Vector2Int> CalculateFreeRooms()
    {
        List<Vector2Int> freeRooms = new List<Vector2Int>();
        for (int i = 0; i < rooms.Count; i++)
        {
            freeRooms.AddRange(rooms[i].freeSpots(rooms));
        }
        return freeRooms;
    }
    void DrawRooms()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            rooms[i].gameObject = Instantiate(smallPrefab, this.gameObject.transform);
            rooms[i].gameObject.transform.position = new Vector3(rooms[i].pos.x * roomSpace, rooms[i].pos.y * roomSpace, 0);
        }
    }

}
