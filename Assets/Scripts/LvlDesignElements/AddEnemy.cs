using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnemy : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public GameObject Spawn()
    {
        GameObject enemy = Instantiate(enemyToSpawn, transform.parent);
        enemy.transform.position = transform.position;
        Destroy(gameObject);
        return enemy;
    }

}
