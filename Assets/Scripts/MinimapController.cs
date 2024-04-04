using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MiniMapController : MonoBehaviour
{
    public GameObject player;
    public Sprite roomIconSprite;
    public Sprite startRoomIconSprite;
    public Sprite shopRoomIconSprite;
    public Sprite bossRoomIconSprite;
    public float iconSize = 10f;
    public float roomSpacing = 20f;
    private RectTransform miniMapRect;
    private Image playerIcon;

    private Dictionary<RoomType, Sprite> roomIconsDict = new Dictionary<RoomType, Sprite>();
    private List<Image> roomIcons = new List<Image>();

    private Vector2 lastPlayerPos;

    public DungeonGenerator dungeonGenerator;

    void Start()
    {
        roomIconsDict.Add(RoomType.STARTROOM, startRoomIconSprite);
        roomIconsDict.Add(RoomType.ROOM, roomIconSprite);
        roomIconsDict.Add(RoomType.SHOPROOM, shopRoomIconSprite);
        roomIconsDict.Add(RoomType.BOSSROOM, bossRoomIconSprite);

        miniMapRect = GetComponent<RectTransform>();

        playerIcon = new GameObject("PlayerIcon").AddComponent<Image>();
        playerIcon.sprite = roomIconSprite;
        playerIcon.transform.SetParent(transform);
        playerIcon.rectTransform.sizeDelta = new Vector2(iconSize, iconSize);
    }

    void DrawMiniMap()
{
    foreach (DungeonRoom room in dungeonGenerator.rooms)
    {
        Vector2 miniMapPos = new Vector2(room.pos.x * (iconSize + roomSpacing), room.pos.y * (iconSize + roomSpacing));

        // Sprawdź odległość pomiędzy graczem a pokojem
        float distanceToPlayer = Vector2.Distance(player.transform.position, miniMapPos);

        // Sprawdź, czy pokój mieści się w zasięgu widoku minimapy
        if (distanceToPlayer <= miniMapRect.rect.width / 2f) // Załóżmy, że minimapa jest kwadratem
        {
            Image roomIcon = new GameObject("RoomIcon").AddComponent<Image>();
            roomIcon.sprite = GetRoomSprite(room.roomType);
            roomIcon.transform.SetParent(transform);
            roomIcon.rectTransform.sizeDelta = new Vector2(iconSize, iconSize);
            roomIcon.rectTransform.anchoredPosition = miniMapPos;

            roomIcons.Add(roomIcon);
        }
    }
}

    Sprite GetRoomSprite(RoomType roomType)
    {
        if (roomIconsDict.ContainsKey(roomType))
            return roomIconsDict[roomType];
        else
            return roomIconSprite;
    }

    void Update()
    {
        if (dungeonGenerator.rooms.Count > 0 && roomIcons.Count == 0)
        {
            DrawMiniMap();
        }

        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 playerRoomPos = new Vector2(Mathf.Round(playerPos.x / (iconSize + roomSpacing)), Mathf.Round(playerPos.y / (iconSize + roomSpacing)));

        Vector2 playerIconPos = new Vector2(playerRoomPos.x * (iconSize + roomSpacing), playerRoomPos.y * (iconSize + roomSpacing));
        playerIcon.rectTransform.anchoredPosition = playerIconPos;

        if (playerRoomPos != lastPlayerPos)
        {
            Vector2 mapMove = lastPlayerPos - playerRoomPos;
            foreach (Image roomIcon in roomIcons)
            {
                roomIcon.rectTransform.anchoredPosition += mapMove * (iconSize + roomSpacing);
            }
            lastPlayerPos = playerRoomPos;
        }
    }
}
