using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickableInstnatnHealth : PicableItem

{

    [SerializeField] private int healingAmount = 1; 
    protected override void onItemPick(GameObject obj)
    {
       PlayerEntityController player=obj.GetComponent<PlayerEntityController>();
       if (player != null)
       {
           player.heal(healingAmount);
       }
    }
}
