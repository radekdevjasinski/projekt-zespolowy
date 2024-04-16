using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowRoom : MonoBehaviour
{
    private DungeonRoom activePlayerRoom;
    private DungeonGenerator dungeonGenerator;
    private GameObject player;
    public float smallRoomCameraSize = 3;
    public float bossRoomCameraSize = 8;
    public float mediumRoomCameraSize = 4;
    private void Start()
    {
        dungeonGenerator = GameObject.Find("Dungeon").GetComponent<DungeonGenerator>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        activePlayerRoom = player.GetComponent<TeleportToNextRoom>().activePlayerRoom;
        if (activePlayerRoom != null)
        {
            Vector2 roomPos = activePlayerRoom.pos * dungeonGenerator.roomSpace;
            transform.position = new Vector3(roomPos.x, roomPos.y, transform.position.z);
            switch(activePlayerRoom.roomType)
            {
                case RoomType.BOSSROOM:
                    GetComponent<Camera>().orthographicSize = bossRoomCameraSize;
                    break;
                case RoomType.SHOPROOM:
                    GetComponent<Camera>().orthographicSize = smallRoomCameraSize;
                    break ;
                default:
                    GetComponent<Camera>().orthographicSize = mediumRoomCameraSize;
                    break;
            }
        }
    }
}
