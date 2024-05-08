using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : EnemyBase
{
    [SerializeField] private float attackSpeed = 3;
    [SerializeField] private float attackRange = 1.5f;
    public PlayerEntityController playerController;
    public float attackSpeedModifier = 1;
    public float attackRangeModifier = 1;
    private Animator animator;
    private KnightPathfinding knightPathfinding;
    private bool canAttack = true;
    protected override void Start()
    {
        base.Start();
        currentHealthPoints = maxHelathPoints;
        damage = 1;
        animator = GetComponent<Animator>();
        knightPathfinding = GetComponent<KnightPathfinding>();
        knightPathfinding.agent.speed = this.speed;
        playerController = GameObject.Find("Player").GetComponent<PlayerEntityController>();
    }

    void FixedUpdate()
    {
        if (!LockMovement)
        {
            if (!IsWithinRange())
            {
                Move();
            }
            if (IsWithinRange() & canAttack)
            {
                Attack();
                animator.SetTrigger("isAttacking");
                canAttack = false;
                StartCoroutine(attackCooldown());

            }
        }
        

    }
    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
    protected override void Move()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)playerTransform.position + direction;
        Vector2 forceDirection = (targetPosition - (Vector2)transform.position).normalized;
        if (rb != null)
        {
            knightPathfinding.Move();
            animator.SetFloat("moveX", forceDirection.x);
            animator.SetFloat("moveY", forceDirection.y);
            animator.Play("Move");
        }
    }

    protected override void Attack()
    {
        PlayerAttributesController playerAttributesController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttributesController>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Vector2 damageDirection = -(collider.transform.position - transform.position).normalized;
                playerController.dealDamage(damage, damageDirection);
                animator.Play("Attack");
                Debug.Log("Hitted Player");
            }
        }
    }

    protected override bool IsWithinRange()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        return distanceToPlayer <= attackRange;
    }



    public void Modify(float mod)
    {
        attackSpeedModifier = mod;
        attackRangeModifier = mod;
        attackSpeed *= attackSpeedModifier;
        attackRange *= attackRangeModifier;
    }
}