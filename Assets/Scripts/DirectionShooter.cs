using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform shootDirection;
    [SerializeField] private float coolDown;
    [SerializeField] private int damage;
    [SerializeField] private int range;
    [SerializeField] private float projectileSpeed;
    private void Start()
    {
        shoot();
    }

    private void shoot()
    {
        //Debug.Log("Shoot");
        Vector3 startPosition = this.shootPoint.position;
        GameObject shootable = Instantiate(
            projectile,
            startPosition,
            new Quaternion(0f, 0f, 0f, 0f)
        );
        shootable.GetComponent<Projectile>().setupProjectileParams(
            this.damage,
            this.range
        );
        Vector3 direction = this.shootDirection.position - this.shootPoint.position;
        shootable.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed, ForceMode2D.Impulse);


        Invoke("shoot",coolDown);
    }

}
