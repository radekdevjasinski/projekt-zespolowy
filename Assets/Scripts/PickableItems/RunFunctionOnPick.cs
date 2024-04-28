using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunFunctionOnPick : MonoBehaviour, IonItemPickUP
{
    Action functionToRUn=new Action(() => { });

    public void addFunction(Action action)
    {
        functionToRUn += action;
    }


    public void onItemPicked(GameObject gameobject)
    {
        functionToRUn.Invoke();
    }
}
