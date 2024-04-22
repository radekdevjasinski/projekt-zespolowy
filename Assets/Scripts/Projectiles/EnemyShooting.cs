using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float fireRate = 0.25f;
    [SerializeField] Transform player;
    [SerializeField] EnemyBase enemy;

    private bool playerInRange = false;
    private bool canShoot = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<ArcherPatrol>();
    }

    void Update()
    {
        if (!enemy.LockMovement)
        {
            if (playerInRange && canShoot)
            {
                Shoot();
                canShoot = false;
                StartCoroutine(shootCooldown());
            }
        }
        
    }
    IEnumerator shootCooldown()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            ArcherPatrol archerPatrol = GetComponentInParent<ArcherPatrol>();
            if (archerPatrol)
            {
                archerPatrol.SetVelocityToZero();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Player")))
            {
                playerInRange = false;
                ArcherPatrol archerPatrol = GetComponentInParent<ArcherPatrol>();
                if (archerPatrol)
                {
                    archerPatrol.SetVelocity();
                }
            }
        }
    }

    void Shoot()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject bullet = Instantiate(prefab, transform.position, rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * 10f;
    }
}
