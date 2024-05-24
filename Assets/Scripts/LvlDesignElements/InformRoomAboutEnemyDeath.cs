using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformRoomAboutEnemyDeath : MonoBehaviour, DeathSeuance
{



  

    public void onDeath()
    {
        Debug.Log(transform.name+ ": InformRoomAboutEnemyDeath");
        transform.parent.GetComponent<EnemiesRoom>().decreaseEnemyCount();
    }
}
