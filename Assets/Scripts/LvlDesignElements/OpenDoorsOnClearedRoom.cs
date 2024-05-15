using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorsOnClearedRoom : MonoBehaviour, IOnRoomCleared
{
    public void onRoomCleared()
    {
        GetComponent<Room>().openAllDoors();
    }
}
