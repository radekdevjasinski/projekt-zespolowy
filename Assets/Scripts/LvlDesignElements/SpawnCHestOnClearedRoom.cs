using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCHestOnClearedRoom : MonoBehaviour, IOnRoomCleared
{
    [SerializeField] private ChestSpawner chestSpawner;

    public void onRoomCleared()
    {
        Debug.Log("attemt to sumon chest");
      chestSpawner.SpawnChest();
    }
}
