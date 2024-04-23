using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
    public int enemiesCount;


    public DungeonRoom(int id, Vector2Int pos, RoomType roomType)
    {
        this.id = id;
        this.pos = pos;
        this.roomType = roomType;
        enemiesCount = 0;
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
    public void OpenRightDoors(List<DungeonRoom> rooms)
    {
        if (gameObject != null)
        {
            Vector2Int[] directions = { new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(0, -1) };
            Door[] doors = gameObject.transform.GetComponentsInChildren<Door>();
            Teleport[] triggers = gameObject.transform.GetComponentsInChildren<Teleport>();
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
                if (taken)
                {
                    doors[i].OpenDoor();
                    triggers[i].active = true;
                    doors[i].gameObject.GetComponent<Collider2D>().enabled = false;

                }
                else
                {
                    doors[i].HideDoor();
                    triggers[i].active = false;
                    doors[i].gameObject.GetComponent<Collider2D>().enabled = true;
                }
            }
        }
    }
    public void CloseAllDoors()
    {
        Door[] doors = gameObject.transform.GetComponentsInChildren<Door>();
        Teleport[] triggers = gameObject.transform.GetComponentsInChildren<Teleport>();
        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i].doorState == DoorState.OPENED)
            {
                doors[i].CloseDoor();
                triggers[i].active = false;
                doors[i].gameObject.GetComponent<Collider2D>().enabled = true;
            }
        }
    }
    public void CountEnemies()
    {
        AddEnemy[] enemies = gameObject.transform.GetComponentsInChildren<AddEnemy>();
        enemiesCount = enemies.Length;
    }
}
public class DungeonGenerator : MonoBehaviour
{
    public int roomSpace = 25;

    public int roomCount;
    public int shopRoomCount;
    public int bossRoomCount;

    public GameObject[] startRoomPrefabs;
    public GameObject[] roomPrefabs;
    public GameObject[] bossPrefabs;
    public GameObject[] shopPrefabs;

    public List<DungeonRoom> rooms = new List<DungeonRoom>();
    public NavigationBake navigationBake;

    void Start()
    {
        int allRooms = roomCount + shopRoomCount + bossRoomCount;
        List<Vector2Int> freeRooms;


        rooms.Add(new DungeonRoom(0, new Vector2Int(0, 0), RoomType.STARTROOM));


        for (int i = 0; i < allRooms; i++)
        {
            freeRooms = CalculateFreeRooms();
            if (roomCount > 0)
            {
                rooms.Add(new DungeonRoom(roomCount, freeRooms[UnityEngine.Random.Range(0, freeRooms.Count)], RoomType.ROOM));
                roomCount--;
            }
            else if (shopRoomCount > 0)
            {
                rooms.Add(new DungeonRoom(roomCount, freeRooms[UnityEngine.Random.Range(0, freeRooms.Count)], RoomType.SHOPROOM));
                shopRoomCount--;
            }
            else if (bossRoomCount > 0)
            {
                AddBossRoom(freeRooms);
                bossRoomCount--;
            }
        }
        DrawRooms();
        foreach (DungeonRoom room in rooms)
        {
            room.OpenRightDoors(rooms);
        }
        navigationBake.BakeNavMesh();
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
            switch(rooms[i].roomType)
            {
                case RoomType.STARTROOM:
                    rooms[i].gameObject = Instantiate(startRoomPrefabs[UnityEngine.Random.Range(0, startRoomPrefabs.Length)], this.gameObject.transform);
                    rooms[i].gameObject.transform.position = new Vector3(rooms[i].pos.x * roomSpace, rooms[i].pos.y * roomSpace, 0);
                    break;
                case RoomType.ROOM:
                    rooms[i].gameObject = Instantiate(roomPrefabs[UnityEngine.Random.Range(0, roomPrefabs.Length)], this.gameObject.transform);
                    rooms[i].gameObject.transform.position = new Vector3(rooms[i].pos.x * roomSpace, rooms[i].pos.y * roomSpace, 0);
                    break;
                case RoomType.BOSSROOM:
                    rooms[i].gameObject = Instantiate(bossPrefabs[UnityEngine.Random.Range(0, bossPrefabs.Length)], this.gameObject.transform);
                    rooms[i].gameObject.transform.position = new Vector3(rooms[i].pos.x * roomSpace, rooms[i].pos.y * roomSpace, 0);
                    break;
                case RoomType.SHOPROOM:
                    rooms[i].gameObject = Instantiate(shopPrefabs[UnityEngine.Random.Range(0, shopPrefabs.Length)], this.gameObject.transform);
                    rooms[i].gameObject.transform.position = new Vector3(rooms[i].pos.x * roomSpace, rooms[i].pos.y * roomSpace, 0);
                    break;

            }
            rooms[i].CountEnemies();
        }
    }
    void AddBossRoom(List<Vector2Int> freeSpots)
    {
        float bestDistance = 0;
        Vector2Int spot = Vector2Int.zero;
        for (int i = 0; i < freeSpots.Count; i++)
        {
            float distance = Vector2Int.Distance(freeSpots[i], Vector2Int.zero);
            if (distance > bestDistance)
            {
                bestDistance = distance;
                spot = freeSpots[i];
            }
        }
        rooms.Add(new DungeonRoom(roomCount, spot, RoomType.BOSSROOM));
    }

}
