using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOnRoomClered : MonoBehaviour, IOnRoomCleared
{
    private Action aciton;

    public void setup(Action action)
    {
        this.aciton= action;
    }


    public void onRoomCleared()
    {
     aciton.Invoke();;
    }
}
