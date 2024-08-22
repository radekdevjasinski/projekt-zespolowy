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
    private NavMeshAgent agent;

    private bool isInRange = false;
    private Knockback knockback;

    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private GameObject burst;
    [SerializeField] private float spawnCooldown = 3.0f;
    [SerializeField] private int spawnCount = 2;

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
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        StartCoroutine(SpawnEnemy());

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
    }

    public void ApplyKnockback(Vector2 direction, float knockbackForce, float knockbackDuration)
    {
        knockback?.ApplyKnockback(direction, knockbackForce, knockbackDuration);
    }

    protected override void Move()
    {
        priestPathFinding.Move();
        animator.SetFloat("moveX", agent.velocity.x);
        float speed = agent.velocity.magnitude;
        animator.SetFloat("speed", speed);

        //animator.Play("Move");
    }

    protected override void Attack()
    {
        playerController.dealDamage(damage);
        ApplyKnockback((transform.position - GameObject.Find("Player").transform.position).normalized, 
            knockback.knockbackForceAttacking, knockback.knockbackDurationAttacking);
    }

    IEnumerator SpawnEnemy()
    {
        priestPathFinding.isCasting = true;
        animator.SetTrigger("summon");
        yield return new WaitForSeconds(spawnCooldown);
        StartCoroutine(SpawnEnemy());

    }
    public void Spawning()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = transform.position + (Vector3)(Random.insideUnitCircle * 2);
            Vector3 roomPosition = transform.parent.position;
            spawnPosition = new Vector3(
                Mathf.Clamp(spawnPosition.x, roomPosition.x - 5.5f, roomPosition.x + 5.5f),
                Mathf.Clamp(spawnPosition.y, roomPosition.y - 2.5f, roomPosition.y + 2.5f),
                spawnPosition.z);
            GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
            UnlockMovement(spawnedEnemy);
            spawnedEnemy.GetComponent<EnemyBase>().dropItemChance = 0;
            
            GameObject spawnedBurst = Instantiate(burst, spawnPosition, Quaternion.identity);
            spawnedBurst.GetComponent<ParticleSystem>().Play();
            Destroy(spawnedBurst, 2f);
        
        
        }
        priestPathFinding.isCasting = false;
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
    public void onDamage()
    {
        ApplyKnockback((transform.position - playerController.gameObject.transform.position).normalized, knockback.knockbackForceAttacked, knockback.knockbackDurationAttacked);
    }
}
