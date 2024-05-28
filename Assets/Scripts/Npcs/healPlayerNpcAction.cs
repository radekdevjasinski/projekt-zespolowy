using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healPlayerNpcAction : MonoBehaviour, InpcAction
{

    private PlayerEntityController controller;
    [SerializeField] private GameObject Affect;
    private void Awake()
    {
        Debug.Log("Heal npc Awake");
        controller = GameObject.Find("Player").GetComponent<PlayerEntityController>();
    }

    public bool perfromAction()
    {
        Debug.Log("Heal npc action");
        
        if (controller.getHealth() >= controller.getMaxHealth())
            return false;
        controller.heal();
        IonPerformAction[] onAction =GetComponents<IonPerformAction>();
        foreach (IonPerformAction action in onAction)
        {
            action.onPerformAction();
        }

        Instantiate(Affect, controller.GetComponentInChildren<SpriteRenderer>().transform);
        return true;

    }
}
