using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TestRangedEnemy : EnemyBase
{
    [SerializeField] private GameObject bombPrefab;
    private float bombSpeed = 3f;
    private float nextBombTimer = 1.5f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        visionRange = 7f;
        timer = nextBombTimer;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = nextBombTimer;
            if (IsWithinRange())
            {
                Attack();
            }
        }
    }
    protected override void Move()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)playerTransform.position - direction;
        Vector2 direction2 = (targetPosition - (Vector2)transform.position).normalized;
        transform.Translate(direction2 * speed * Time.deltaTime);
    }

    protected override void Attack()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bombSpeed;
    }
}
