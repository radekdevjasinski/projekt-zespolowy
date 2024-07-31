using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnemy : MonoBehaviour
{
    [SerializeField] private bool spwanAtStart;
    public GameObject enemyToSpawn;

    private void Start()
    {
        if (spwanAtStart)
        {
            Spawn();
        }
    }

    public GameObject Spawn()
    {
        GameObject enemy = Instantiate(enemyToSpawn, transform.parent);
        enemy.transform.position = transform.position;
        Destroy(gameObject);
        return enemy;
    }

}
