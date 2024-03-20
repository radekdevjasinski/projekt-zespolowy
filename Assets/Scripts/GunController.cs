using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    float fireForce = 20f;

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce
            (firePoint.up *  fireForce, ForceMode2D.Impulse);
    }
}
