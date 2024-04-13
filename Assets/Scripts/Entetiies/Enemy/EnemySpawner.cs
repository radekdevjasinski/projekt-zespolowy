using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        Vector3 position1 = transform.position + Quaternion.Euler(0, 0, 120) * Vector3.right;
        Vector3 position2 = transform.position + Quaternion.Euler(0, 0, -120) * Vector3.right;
        Vector3 position3 = transform.position + Vector3.up;
        Instantiate(enemyPrefab, position1, Quaternion.identity);
        Instantiate(enemyPrefab, position2, Quaternion.identity);
        Instantiate(enemyPrefab, position3, Quaternion.identity);
    }
}
