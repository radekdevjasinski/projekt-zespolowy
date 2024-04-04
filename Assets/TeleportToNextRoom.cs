using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToNextRoom : MonoBehaviour
{
    public DungeonGenerator dungeonGenerator;
    public DungeonRoom activePlayerRoom;
    public void Teleport(Vector2 triggerPos)
    {
        if (activePlayerRoom == null)
        {
            activePlayerRoom = dungeonGenerator.rooms[0];
        }
        for (int i = 0; i < dungeonGenerator.rooms.Count; i++)
        {
            if (activePlayerRoom.pos + triggerPos == dungeonGenerator.rooms[i].pos)
            {
                activePlayerRoom = dungeonGenerator.rooms[i];
                Vector2 playerPos = activePlayerRoom.pos * dungeonGenerator.roomSpace;
                this.gameObject.transform.position = new Vector3(playerPos.x, playerPos.y, 0);
                break;
            }
        }
    }
}
