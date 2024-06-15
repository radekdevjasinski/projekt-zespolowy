using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAddEnemyAtStart : MonoBehaviour
{
    void Start()
    {
        GetComponent<AddEnemy>().Spawn();
    }

}
