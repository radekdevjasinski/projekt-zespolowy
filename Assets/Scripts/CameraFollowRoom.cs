using Cinemachine;
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

    [Header("Cameras")]
    public GameObject mainCamera;
    public CinemachineVirtualCamera bossRoomCamera;

    [Header("SwipeAnimation")]
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        dungeonGenerator = GameObject.Find("Dungeon").GetComponent<DungeonGenerator>();
        player = GameObject.Find("Player");

        transform.position = new Vector3(transform.position.x, transform.position.y, -11);
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
                    bossRoomCamera.gameObject.SetActive(true);

                    GameObject bossRoomPrefab = GameObject.FindGameObjectWithTag("LichBossRoom");
                    if (bossRoomPrefab != null)
                    {
                        PolygonCollider2D roomCollider = bossRoomPrefab.GetComponentInChildren<PolygonCollider2D>();
                        if (roomCollider != null)
                        {
                            bossRoomCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = roomCollider;
                        }
                        else
                        {
                            Debug.LogError("Nie znaleziono Collidera 2D w LichBossRoom.");
                        }
                    }
                    else
                    {
                        Debug.LogError("Nie znaleziono prefaba LichBossRoom na scenie.");
                    }

                    break;
                default:
                    bossRoomCamera.gameObject.SetActive(false);
                    mainCamera.GetComponent<Camera>().orthographicSize = activePlayerRoom.roomType == RoomType.SHOPROOM ? smallRoomCameraSize : mediumRoomCameraSize;
                    break;
            }
        }
    }
}
