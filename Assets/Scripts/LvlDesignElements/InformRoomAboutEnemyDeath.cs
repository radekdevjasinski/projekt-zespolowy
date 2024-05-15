using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformRoomAboutEnemyDeath : MonoBehaviour, DeathSeuance
{

    private Action onDeathACtion;

    public void setup(Action newAction)
    {
        onDeathACtion=newAction;
    }

    public void onDeath()
    {
        onDeathACtion.Invoke();
    }
}
