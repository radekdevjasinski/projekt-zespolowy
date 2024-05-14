using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesRoom : Room
{
    private int enemiesCount;

    private void Awake()
    {
        enemiesCount = GetComponentsInChildren<AddEnemy>().Length;
    }


    void decreaseEnemyCount()
    {
        enemiesCount--;
    }

   

    private void onCleared()
    {
        foreach (IOnRoomCleared var in GetComponents<IOnRoomCleared>())
        {
            var.onRoomCleared();
        }
    }

    public override void onEntry()
    {
        base.onEntry();
        this.closeAllDorrs();
    }


}
