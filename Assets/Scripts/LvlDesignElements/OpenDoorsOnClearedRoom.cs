using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorsOnClearedRoom : MonoBehaviour, IOnRoomCleared
{
    public void onRoomCleared()
    {
        GetComponent<Room>().openAllDoors();
        DungeonGenerator.instance.rooms[DungeonGenerator.instance.getCurrRoom().pos].visited = true;
        SaveLoadManager.instance.SaveGame();
    }
}
