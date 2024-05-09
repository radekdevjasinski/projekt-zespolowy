using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healPlayerNpcAction : MonoBehaviour, InpcAction
{
    public bool perfromAction()
    {
        PlayerEntityController controller = GameObject.Find("Player").GetComponent<PlayerEntityController>();
        if (controller.getHealth() >= controller.getMaxHealth())
            return false;
        controller.heal();
        IonPerformAction[] onAction =GetComponents<IonPerformAction>();
        foreach (IonPerformAction action in onAction)
        {
            action.onPerformAction();
        }
        return true;

    }
}
