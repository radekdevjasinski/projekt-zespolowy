using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonChestOnStart : MonoBehaviour
{
    void Start()
    {
        GetComponent<ChestSpawner>().SpawnChest();
    }

 
}
