using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public enum RoomType
{
    STARTROOM,
    ROOM,
    SHOPROOM,
    BOSSROOM
}
public class DungeonRoom 
{
    public Vector2Int pos;
    public RoomType roomType;
    public GameObject gameObject;
    public int enemiesCount;
    public bool visited;


    public DungeonRoom( Vector2Int pos, RoomType roomType)
    {
        this.pos = pos;
        this.roomType = roomType;
        enemiesCount = 0;
        visited = false;
        gameObject = null;
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


    public void CountEnemies()
    {
        AddEnemy[] enemies = gameObject.transform.GetComponentsInChildren<AddEnemy>();
        enemiesCount = enemies.Length;
    }

    public override int GetHashCode()
    {
        return pos.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override string ToString()
    {
        return this.pos+": "+this.roomType;
    }


}

public class DungeonGenerator : MonoBehaviour
{
    private CameraFollowRoom cameraController;
    private static DungeonGenerator _instance;

    public static DungeonGenerator instance
    {
        get => _instance;
    }

    private void Awake()
    {
        if (_instance != null)
            throw new Exception("thereshould be only one instanece of dungeon geneartor");
        _instance = this;
    }


    public int roomSpace = 25;

    private static readonly List<Vector2Int> directions = new List<Vector2Int>
    {
        new Vector2Int(0, 1),
        new Vector2Int(-1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0)
    };

    public int roomCount;
    public int shopRoomCount;
    public int bossRoomCount;

    public GameObject[] startRoomPrefabs;
    public GameObject[] roomPrefabs;
    public GameObject[] bossPrefabs;
    public GameObject[] shopPrefabs;

    public Dictionary<Vector2Int,DungeonRoom> rooms = new Dictionary<Vector2Int, DungeonRoom>();
    public HashSet<Vector2Int> freeSpots = new HashSet<Vector2Int>(){new Vector2Int(0,0)};
    public NavigationBake navigationBake;

    public void addRoomBase(Vector2Int postion, RoomType type, bool visited = false)
    {
    
        freeSpots.Remove(postion);
        rooms.Add(postion,new DungeonRoom(postion, type) { visited = visited });
        foreach (Vector2Int dir in directions)
        {
            if (!rooms.ContainsKey(dir+ postion))
            {
                freeSpots.Add(dir + postion);
            }
        }
        //Debug.Log("free spts: " + string.Join(", ", freeSpots));
        //Debug.Log("rooms: " + string.Join(", ",rooms.Keys));
    }


    void Start()
    {
        cameraController = GameObject.Find("CameraHolder").GetComponent<CameraFollowRoom>();
        if (cameraController == null)
            throw new Exception("no cam,era hlder");
        int allRooms = roomCount + shopRoomCount + bossRoomCount;

        addRoomBase(new Vector2Int(0, 0), RoomType.STARTROOM, true);
 
        for (int i = 0; i < allRooms; i++)
        {
            Debug.Log("Adding room nr: "+ i);
            if (roomCount > 0)
            {
                Debug.Log("Adding room : room");
                addRoomBase(freeSpots.ElementAt(UnityEngine.Random.Range(0, freeSpots.Count)),RoomType.ROOM);
                roomCount--;
            }
            else if (shopRoomCount > 0)
            {
                Debug.Log("Adding room : shop");
                addRoomBase(freeSpots.ElementAt(UnityEngine.Random.Range(0, freeSpots.Count)), RoomType.SHOPROOM);

                shopRoomCount--;
            }
            else if (bossRoomCount > 0)
            {
                Debug.Log("Adding room : boss");
                Vector2Int positon = getFarthestOpenSpot();
                addRoomBase(positon, RoomType.BOSSROOM);
                bossRoomCount--;
            }
        }
        DrawRooms();
        setupDriadSpawnPoint();
        setupRooms();
        navigationBake.BakeNavMesh();
    }



    void DrawRooms()
    {

        List<Vector2Int> keys = rooms.Keys.ToList();
        //creates rooms
        foreach (Vector2Int key in keys)
        {
            DungeonRoom item=rooms[key];
            Debug.Log("Drawign room: "+ item);
            GameObject roomPrefab= startRoomPrefabs[0];
            switch (item.roomType)
            {
              case RoomType.STARTROOM:
                  roomPrefab = startRoomPrefabs[UnityEngine.Random.Range(0, startRoomPrefabs.Length)];

                    break;
                case RoomType.ROOM:
                    roomPrefab = roomPrefabs[UnityEngine.Random.Range(0, roomPrefabs.Length)];
                    break;
                case RoomType.BOSSROOM:
                    roomPrefab = bossPrefabs[UnityEngine.Random.Range(0, bossPrefabs.Length)]; break;
                case RoomType.SHOPROOM:
                    roomPrefab = shopPrefabs[UnityEngine.Random.Range(0, shopPrefabs.Length)];
                    break;
            }
            item.gameObject = Instantiate(roomPrefab, this.gameObject.transform);
            item.gameObject.transform.position = new Vector3(item.pos.x * roomSpace, item.pos.y * roomSpace, 0);
            rooms[key] = item;
        }
    }

    void setupRooms()
    {
        List<Vector2Int> keys = rooms.Keys.ToList();
        //setupRooms
        foreach (Vector2Int key in keys)
        {
            DungeonRoom item = rooms[key];
            item.gameObject.GetComponent<Room>().setupNewRoom(getRoomsAround(key),key);

           

        }
    }

    void setupDriadSpawnPoint()
    {
        List<DiradSpawnPointOnEntry> driadEntires=rooms.Values.ToList()
            .FindAll((room => room.gameObject.GetComponent<DiradSpawnPointOnEntry>()!=null))
            .Select((room =>room.gameObject.GetComponent<DiradSpawnPointOnEntry>() ))
            .ToList();
        driadEntires.ForEach((entry => entry.setApperanceChance(0)));
        driadEntires[UnityEngine.Random.Range(0,driadEntires.Count-1)].setApperanceChance(1);
    }

    //return table with room Configuration around that room in order left, up , right, down
    DungeonRoom[] getRoomsAround(Vector2Int postion)
    {
        Vector2Int leftPositon = postion + new Vector2Int(-1, 0);
        Vector2Int upPosition = postion + new Vector2Int(0, 1);
        Vector2Int rightPositon = postion + new Vector2Int(1, 0);
        Vector2Int downPosiotn = postion + new Vector2Int(0, -1);

        return new DungeonRoom[]
        {
            rooms.ContainsKey(leftPositon) ? rooms[leftPositon] : null,
            rooms.ContainsKey(upPosition) ? rooms[upPosition] : null,
            rooms.ContainsKey(rightPositon) ? rooms[rightPositon] : null,
            rooms.ContainsKey(downPosiotn) ? rooms[downPosiotn] : null
        };
    }


    Vector2Int getFarthestOpenSpot()
    {
        float bestDistance = 0;
        Vector2Int spot = freeSpots.OrderByDescending(x=>x.magnitude).First();
        return spot;
    }

    #region Dungeon controls
    Vector2Int currPlayerPOsiton=Vector2Int.zero;
    [SerializeField] private float controlsOfTime = 0.8f;

    public float getControlsOFTime()
    {
        return controlsOfTime;
    }
    public void onRoomEnter(Vector2Int positon)
    {
        Debug.Log("onRoomEnter");
        if(rooms.ContainsKey(positon))
        EnterRoom(rooms[positon]);
    }
    public void EnterRoom(DungeonRoom room)
    {
        updateCamera(room);
        currPlayerPOsiton = room.pos;
        if (!room.visited)
        {
            room.visited = true;
        }
    }

    void updateCamera(DungeonRoom room)
    {
        Debug.Log("udpate camera" + room.roomType);
        if (room.roomType == RoomType.BOSSROOM)
        {
            cameraController.cameraFadeIn();
            cameraController.enableConfirer();
        }
        else
        {
            cameraController.disableConfirer();
        }
        cameraController.setCameraFollowPonit(room.gameObject.GetComponent<Room>().getCameraFollowPoint());
        cameraController.setCameraSize(room.gameObject.GetComponent<Room>().getRoomSize());
        //cameraController.moveCameraToPosition(room);
    }

    public DungeonRoom getCurrRoom()
    {
        return rooms[currPlayerPOsiton];
    }

    #endregion



}
