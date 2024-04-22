using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowRoom : MonoBehaviour
{
    [Header("DungeonRoomsCamera")]
    private DungeonRoom activePlayerRoom;
    private DungeonGenerator dungeonGenerator;
    private GameObject player;
    public float smallRoomCameraSize = 3;
    public float bossRoomCameraSize = 8;
    public float mediumRoomCameraSize = 4;
    [Header("SwipeAnimation")]
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    private void Start()
    {
        dungeonGenerator = GameObject.Find("Dungeon").GetComponent<DungeonGenerator>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        activePlayerRoom = player.GetComponent<PlayerTeleporter>().activePlayerRoom;
        if (activePlayerRoom != null)
        {
            Vector2 roomPos = activePlayerRoom.pos * dungeonGenerator.roomSpace;
            transform.position = Vector3.SmoothDamp(transform.position,
                new Vector3(roomPos.x, roomPos.y, transform.position.z), ref velocity, smoothTime);
            switch (activePlayerRoom.roomType)
            {
                case RoomType.BOSSROOM:
                    GetComponentInChildren<Camera>().orthographicSize = bossRoomCameraSize;
                    break;
                case RoomType.SHOPROOM:
                    GetComponentInChildren<Camera>().orthographicSize = smallRoomCameraSize;
                    break ;
                default:
                    GetComponentInChildren<Camera>().orthographicSize = mediumRoomCameraSize;
                    break;
            }
        }
        
    }
}
