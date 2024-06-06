using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float fireRate = 0.25f;
    [SerializeField] float shootPower = 7f;
    [SerializeField] float shootingRange = 4f;

    EnemyBase enemy;
    Transform player;
    ArcherPatrol archerPatrol;

    private bool playerInRange = false;
    private bool canShoot = true;
    private bool isCurrentlyShooting = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<ArcherPatrol>();
        archerPatrol = GetComponentInParent<ArcherPatrol>();
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= shootingRange)
            {
                playerInRange = true;
                if (!enemy.LockMovement && canShoot && !isCurrentlyShooting)
                {
                    StartShooting();
                    canShoot = false;
                    StartCoroutine(shootCooldown());
                }
            }
            else
            {
                playerInRange = false;
                if (!isCurrentlyShooting && archerPatrol)
                {
                    archerPatrol.SetVelocity();
                }
            }
        }
    }

    IEnumerator shootCooldown()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    void StartShooting()
    {
        if (archerPatrol)
        {
            archerPatrol.SetVelocityToZero();
            archerPatrol.isShooting = true;
            isCurrentlyShooting = true;
        }
    }

    public void Shoot()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject bullet = Instantiate(prefab, transform.position, rotation);
        Arrow arrow = bullet.GetComponent<Arrow>();
        if (arrow != null)
        {
            arrow.SetSpawningEnemy(this.gameObject);
        }
        bullet.GetComponent<Rigidbody2D>().velocity = direction * shootPower;

        StartCoroutine(ResetShootingFlag());
    }

    IEnumerator ResetShootingFlag()
    {
        yield return new WaitForSeconds(fireRate);
        if (archerPatrol)
        {
            archerPatrol.isShooting = false;
        }
        isCurrentlyShooting = false;
    }
}
