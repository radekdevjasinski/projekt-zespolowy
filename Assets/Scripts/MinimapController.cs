using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Unity.Collections.LowLevel.Unsafe;

public class MiniMapController : MonoBehaviour
{
    public GameObject player;
    public Sprite roomIconSprite;
    public Sprite startRoomIconSprite;
    public Sprite shopRoomIconSprite;
    public Sprite bossRoomIconSprite;
    public Sprite unknownRoomSprite;
    public float iconSize = 15f;
    public float roomSpacing = 18f;
    private RectTransform miniMapRect;
    private Image playerIcon;

    private Dictionary<RoomType, Sprite> roomIconsDict = new Dictionary<RoomType, Sprite>();
    private Dictionary<Vector2Int, Image> roomIcons = new Dictionary<Vector2Int, Image>();

    public DungeonGenerator dungeonGenerator;

    //public PlayerTeleporter teleportScript;
    private Vector2Int lastPlayerRoomPos;

    void Start()
    {
        roomIconsDict.Add(RoomType.STARTROOM, startRoomIconSprite);
        roomIconsDict.Add(RoomType.ROOM, roomIconSprite);
        roomIconsDict.Add(RoomType.SHOPROOM, shopRoomIconSprite);
        roomIconsDict.Add(RoomType.BOSSROOM, bossRoomIconSprite);

        miniMapRect = GetComponent<RectTransform>();

        //teleportScript = FindObjectOfType<PlayerTeleporter>();

        DungeonRoom startRoom = new DungeonRoom( Vector2Int.zero, RoomType.STARTROOM);
        lastPlayerRoomPos = startRoom.pos;

        StartCoroutine(DelayedDrawMiniMap(startRoom));
    }

    public void DrawMiniMap(DungeonRoom currentPlayerRoom)
    {
        //Debug.Log("Rysuje");
        ClearMiniMap();

        foreach ( KeyValuePair<Vector2Int,DungeonRoom> item in dungeonGenerator.rooms)
        {
            DungeonRoom room = item.Value;
            Vector2Int roomPos = room.pos;

            Vector2Int playerRoomPos = currentPlayerRoom.pos;
            Vector2 miniMapPos = new Vector2((roomPos.x - playerRoomPos.x) * (iconSize + roomSpacing), (roomPos.y - playerRoomPos.y) * (iconSize + roomSpacing));

            Vector2 roomIconSize = new Vector2(iconSize, iconSize);
            if (currentPlayerRoom.Equals(room))
            {
                roomIconSize *= 2f;
            }

            if (IsInMiniMapArea(miniMapPos))
            {
                Image roomIcon = new GameObject("RoomIcon").AddComponent<Image>();
                roomIcon.sprite = GetRoomSprite(room.roomType);
                if (!room.visited)
                {
                    roomIcon.sprite = unknownRoomSprite;
                }
                roomIcon.transform.SetParent(transform);
                roomIcon.rectTransform.sizeDelta = roomIconSize;
                roomIcon.rectTransform.anchoredPosition = miniMapPos;

                roomIcons.Add(roomPos, roomIcon);
            }
        }
    }



    void ClearMiniMap()
    {
        foreach (KeyValuePair<Vector2Int, Image> pair in roomIcons)
        {
            Destroy(pair.Value.gameObject);
        }
        roomIcons.Clear();
    }

    Sprite GetRoomSprite(RoomType roomType)
    {
        if (roomIconsDict.ContainsKey(roomType))
            return roomIconsDict[roomType];
        else
            return roomIconSprite;
    }

    bool IsInMiniMapArea(Vector2 position)
    {
        bool isInArea = Mathf.Abs(position.x) <= miniMapRect.rect.width / 2f && Mathf.Abs(position.y) <= miniMapRect.rect.height / 2f;

        return isInArea;
    }

    IEnumerator DelayedDrawMiniMap(DungeonRoom startRoom)
    {
        yield return null;

        DrawMiniMap(startRoom);
    }

    void Update()
    {
        DungeonRoom currentPlayerRoom = DungeonGenerator.instance.getCurrRoom();


        if (currentPlayerRoom.pos != lastPlayerRoomPos)
        {
            dungeonGenerator.EnterRoom(currentPlayerRoom);

            DrawMiniMap(currentPlayerRoom);

            lastPlayerRoomPos = currentPlayerRoom.pos;
        }
    }
}
