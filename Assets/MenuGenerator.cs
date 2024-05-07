using System.Collections.Generic;
using UnityEngine;

public class MenuGenerator : MonoBehaviour
{
    public GameObject[] startRoomPrefabs;
    public GameObject[] roomPrefabs;

    public int roomSpace = 25;

    void Start()
    {
        GenerateMenu();
    }

    void GenerateMenu()
    {
        List<DungeonRoom> menuRooms = new List<DungeonRoom>();

        // Tworzenie startowego pokoju
        DungeonRoom startRoom = new DungeonRoom(0, new Vector2Int(0, 0), RoomType.STARTROOM);
        menuRooms.Add(startRoom);

        // Tworzenie dwóch dodatkowych pokoi
        DungeonRoom leftRoom = new DungeonRoom(1, new Vector2Int(-1, 0), RoomType.ROOM);
        menuRooms.Add(leftRoom);

        DungeonRoom rightRoom = new DungeonRoom(2, new Vector2Int(0, 1), RoomType.ROOM);
        menuRooms.Add(rightRoom);

        // Rysowanie pokoi
        DrawMenuRooms(menuRooms);
    }

    void DrawMenuRooms(List<DungeonRoom> menuRooms)
    {
        for (int i = 0; i < menuRooms.Count; i++)
        {
            switch (menuRooms[i].roomType)
            {
                case RoomType.STARTROOM:
                    menuRooms[i].gameObject = Instantiate(startRoomPrefabs[Random.Range(0, startRoomPrefabs.Length)], transform);
                    // Otwarcie drzwi z każdej strony w pokoju startowym
                    menuRooms[i].OpenRightDoors(menuRooms);
                    break;
                case RoomType.ROOM:
                    menuRooms[i].gameObject = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)], transform);
                    break;
            }

            // Ustawienie pozycji pokoju
            menuRooms[i].gameObject.transform.position = new Vector3(menuRooms[i].pos.x * roomSpace, menuRooms[i].pos.y * roomSpace, 0);

            // Otwarcie drzwi
            menuRooms[i].OpenRightDoors(menuRooms);
        }
    }
}
