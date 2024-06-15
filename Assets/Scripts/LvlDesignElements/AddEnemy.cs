using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnemy : MonoBehaviour
{
    [SerializeField]
    private bool spwanAtStart=false;

    private void Start()
    {
        if (spwanAtStart)
        {

        }
    }

    public GameObject enemyToSpawn;
    public GameObject Spawn()
    {
        GameObject enemy = Instantiate(enemyToSpawn, transform.parent);
        enemy.transform.position = transform.position;
        Destroy(gameObject);
        return enemy;
    }

}
