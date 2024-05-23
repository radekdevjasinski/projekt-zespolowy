using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGoldenCoinPickupValidaiation : MonoBehaviour ,IitemPickUpValidaitin
{
    public bool canBePicked(GameObject pickingUpActor)
    {
        return !pickingUpActor.GetComponent<PlayerItemsController>().hasGoldenCoin();
    }
}
