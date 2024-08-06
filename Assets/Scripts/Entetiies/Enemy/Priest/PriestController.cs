using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PriestController : EnemyBase, OnDamage
{
    [SerializeField] private float attackRange = 1.5f;
    public PlayerEntityController playerController;
    public float attackRangeModifier = 1;
    private Animator animator;
    private PriestPathFinding priestPathFinding;
    private bool isInRange = false;
    private Knockback knockback;

    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private float spawnCooldown = 5.0f;
    private bool canSpawn = true;

    protected override void Start()
    {
        base.Start();
        currentHealthPoints = maxHelathPoints;
        damage = 1;
        animator = GetComponent<Animator>();
        priestPathFinding = GetComponent<PriestPathFinding>();
        priestPathFinding.agent.speed = this.speed;
        playerController = GameObject.Find("Player").GetComponent<PlayerEntityController>();
        knockback = GetComponent<Knockback>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    void FixedUpdate()
    {
        if (!LockMovement)
        {
            Move();
            if (isInRange)
            {
                Attack();
            }
        }

        if (canSpawn)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    public void ApplyKnockback(Vector2 direction, float knockbackForce, float knockbackDuration)
    {
        knockback?.ApplyKnockback(direction, knockbackForce, knockbackDuration);
    }

    protected override void Move()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + direction;
        Vector2 forceDirection = (targetPosition - (Vector2)transform.position).normalized;

        if (rb != null)
        {
            priestPathFinding.Move();
            animator.SetFloat("moveX", forceDirection.x);
            animator.SetFloat("moveY", forceDirection.y);
            animator.Play("Move");
        }
    }

    protected override void Attack()
    {
        playerController.dealDamage(damage);
        ApplyKnockback((transform.position - GameObject.Find("Player").transform.position).normalized, knockback.knockbackForceAttacking, knockback.knockbackDurationAttacking);
    }

    IEnumerator SpawnEnemy()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnCooldown);

        for (int i = 0; i < 2; i++)
        {
            Vector3 spawnPosition = transform.position + (Vector3)(Random.insideUnitCircle * 2);
            GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
            UnlockMovement(spawnedEnemy);
        }

        canSpawn = true;
    }

    void UnlockMovement(GameObject enemy)
    {
        var zombieController = enemy.GetComponent<ZombieController>();
        if (zombieController != null)
        {
            zombieController.LockMovement = false;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    protected override bool IsWithinRange()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        return distanceToPlayer <= attackRange;
    }

    protected override void onDie()
    {
        base.onDie();
    }

    public void Modify(float mod)
    {
        attackRangeModifier = mod;
        attackRange *= attackRangeModifier;
    }

    public void onDamage()
    {
        ApplyKnockback((transform.position - playerController.gameObject.transform.position).normalized, knockback.knockbackForceAttacked, knockback.knockbackDurationAttacked);
    }
}
