using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToNextRoom : MonoBehaviour
{
    public DungeonRoom activePlayerRoom;
    private DungeonGenerator dungeonGenerator;
    public float distanceFromDoor = 1f;
    private void Start()
    {
        dungeonGenerator = GameObject.Find("Dungeon").GetComponent<DungeonGenerator>();
    }
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
                Vector2 doorDirection = (triggerPos * -1);
                Door[] doors = activePlayerRoom.gameObject.GetComponentsInChildren<Door>();
                Vector2[] directions = { new Vector2(-1, 0), new Vector2(0,1), new Vector2(1,0), new Vector2(0, -1) };
                Door activeDoor = null;
                for (int j = 0; j < directions.Length; j++)
                {
                    if (directions[j] == doorDirection)
                    {
                        if (j < doors.Length)
                        {
                            activeDoor = doors[j];
                        }
                    }
                }
                if (activeDoor != null)
                {
                    Vector3 destination = activeDoor.transform.position + new Vector3(triggerPos.x * distanceFromDoor, triggerPos.y * distanceFromDoor, 0);
                    this.gameObject.transform.position = destination;
                }
                break;
            }
        }
    }

}
