using Cinemachine;
using System.Collections;
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
    private float transitionDuration;
    public bool transitionDone = false;

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
                    if (!transitionDone)
                    {
                        StartCoroutine(DelayedCameraActivation(0.25f));
                    }
                    break;
                default:
                    transitionDone = false;
                    bossRoomCamera.gameObject.SetActive(false);
                    mainCamera.GetComponent<Camera>().orthographicSize = activePlayerRoom.roomType == RoomType.SHOPROOM ? smallRoomCameraSize : mediumRoomCameraSize;
                    break;
            }
        }
    }

    IEnumerator DelayedCameraActivation(float delay)
    {
        transitionDone = true;
        transitionDuration = 5f;
        float t = 0;
        yield return new WaitForSeconds(delay);
        bossRoomCamera.gameObject.SetActive(true);
        bossRoomCamera.GetComponent<CinemachineStoryboard>().m_Alpha = 1;
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
        yield return new WaitForSeconds(delay);
        while (t < transitionDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, t / transitionDuration);
            bossRoomCamera.GetComponent<CinemachineStoryboard>().m_Alpha = alpha;
            yield return null;
        }
    }
}
