using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class MiniMapController : MonoBehaviour
{
    public GameObject player;
    public Sprite roomIconSprite;
    public Sprite shopRoomIconSprite;
    public Sprite bossRoomIconSprite;
    public Sprite unknownRoomSprite;
    public Sprite backgroundImage;
    public Sprite currentRoomSprite;
    public float iconSize = 40f;
    public float roomSpacing = 5f;
    private RectTransform miniMapRect;
    private Image playerIcon;

    private Dictionary<RoomType, Sprite> roomIconsDict = new Dictionary<RoomType, Sprite>();
    private Dictionary<Vector2Int, Image> roomIcons = new Dictionary<Vector2Int, Image>();

    public DungeonGenerator dungeonGenerator;

    private Vector2Int lastPlayerRoomPos;

    void Start()
    {
        roomIconsDict.Add(RoomType.STARTROOM, roomIconSprite);
        roomIconsDict.Add(RoomType.ROOM, roomIconSprite);
        roomIconsDict.Add(RoomType.SHOPROOM, shopRoomIconSprite);
        roomIconsDict.Add(RoomType.BOSSROOM, bossRoomIconSprite);

        miniMapRect = GetComponent<RectTransform>();

        DungeonRoom startRoom = new DungeonRoom(Vector2Int.zero, RoomType.STARTROOM);
        lastPlayerRoomPos = startRoom.pos;

        StartCoroutine(DelayedDrawMiniMap(startRoom));
    }

    public void DrawMiniMap(DungeonRoom currentPlayerRoom)
    {
        ClearMiniMap();

        foreach (KeyValuePair<Vector2Int, DungeonRoom> item in dungeonGenerator.rooms)
        {
            DungeonRoom room = item.Value;
            Vector2Int roomPos = room.pos;

            Vector2Int playerRoomPos = currentPlayerRoom.pos;
            Vector2 miniMapPos = new Vector2((roomPos.x - playerRoomPos.x) * (iconSize + roomSpacing), (roomPos.y - playerRoomPos.y) * (iconSize + roomSpacing));

            Vector2 backgroundIconSize = new Vector2(iconSize + 5, iconSize + 5);
            Vector2 roomIconSize = new Vector2(iconSize, iconSize);

            if (IsInMiniMapArea(miniMapPos))
            {
                GameObject roomIconObject = new GameObject("RoomIcon");
                roomIconObject.transform.SetParent(transform);

                Image roomIcon = roomIconObject.AddComponent<Image>();

                roomIcon.sprite = room.visited ? roomIconSprite : unknownRoomSprite;

                roomIcon.rectTransform.sizeDelta = roomIconSize;
                roomIcon.rectTransform.anchoredPosition = miniMapPos;

                if (room.visited && (room.roomType == RoomType.BOSSROOM || room.roomType == RoomType.SHOPROOM))
                {
                    GameObject specialRoomIconObject = new GameObject("SpecialRoomIcon");
                    specialRoomIconObject.transform.SetParent(roomIconObject.transform);

                    Image specialRoomIcon = specialRoomIconObject.AddComponent<Image>();
                    specialRoomIcon.sprite = room.roomType == RoomType.BOSSROOM ? bossRoomIconSprite : shopRoomIconSprite;
                    specialRoomIcon.rectTransform.sizeDelta = roomIconSize;
                    specialRoomIcon.rectTransform.anchoredPosition = Vector2.zero;
                }

                if (currentPlayerRoom.Equals(room))
                {
                    roomIcon.sprite = currentRoomSprite;
                }

                GameObject backgroundIconObject = new GameObject("BackgroundIcon");
                backgroundIconObject.transform.SetParent(roomIconObject.transform);

                Image backgroundIcon = backgroundIconObject.AddComponent<Image>();
                backgroundIcon.sprite = backgroundImage;
                backgroundIcon.rectTransform.sizeDelta = backgroundIconSize;
                backgroundIcon.rectTransform.anchoredPosition = Vector2.zero;

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
