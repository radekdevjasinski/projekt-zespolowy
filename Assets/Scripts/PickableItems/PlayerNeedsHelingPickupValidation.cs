using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNeedsHelingPickupValidation : MonoBehaviour, IitemPickUpValidaitin
{
    public bool canBePicked(GameObject pickingUpActor)
    {
        PlayerEntityController player = pickingUpActor.GetComponent<PlayerEntityController>();
        if (player != null && player.getMaxHealth()>player.getHealth())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
