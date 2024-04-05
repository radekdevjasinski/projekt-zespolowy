using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootin : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform aim;
    void Start()
    {
        StartCoroutine(ShootRepeatedly(1f));
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(prefab, aim.position, transform.rotation);
    }

    IEnumerator ShootRepeatedly(float interval)
    {
        while (true)
        {
            Shoot(); 
            yield return new WaitForSeconds(interval);
        }
    }
}
