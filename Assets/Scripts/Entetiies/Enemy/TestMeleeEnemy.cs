using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMeleeEnemy : EnemyBase
{
    private float nextAttackTimer = 1f;
    private float range = 0.5f; //zasiêg ataku wroga

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //speed = 3f;
        timer = nextAttackTimer;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = nextAttackTimer;
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
        Vector2 targetPosition = (Vector2)playerTransform.position + direction;
        Vector2 direction2 = (targetPosition - (Vector2)transform.position).normalized;
        transform.Translate(direction2 * speed * Time.deltaTime);
    }

    protected override void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log("Hitted Player");
            }
        }
    }
}
