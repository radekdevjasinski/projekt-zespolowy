using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class KnightController : EnemyBase
{
    [SerializeField] private float attackSpeed = 3;
    [SerializeField] private float attackRange = 1.5f;
    public PlayerEntityController playerController;
    public float attackSpeedModifier = 1;
    public float attackRangeModifier = 1;
    public DamageController damageController;
    private Animator animator;
    private KnightPathfinding knightPathfinding;
    bool isAttacking = false;
    bool isMoving = true;
    void Start()
    {
        timer = attackSpeed;
        currentHealthPoints = healthPoints;
        damage = 1;
        animator = GetComponent<Animator>();
        knightPathfinding = GetComponent<KnightPathfinding>();
        knightPathfinding.agent.speed = this.speed;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEntityController>();
    }

    void FixedUpdate()
    {
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isMoving", isMoving);
        if (isMoving && !IsWithinRange())
        {
            Move();
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = attackSpeed;
            if (IsWithinRange())
            {
                isAttacking = true;
                isMoving = false;
                Attack();
            }
            else
            {
                isAttacking = false;
                isMoving = true;
            }
        }
    }

    protected override void Move()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)playerTransform.position + direction;
        Vector2 forceDirection = (targetPosition - (Vector2)transform.position).normalized;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            knightPathfinding.Move();
            animator.SetFloat("moveX",forceDirection.x);
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
                playerController.dealDamage(damage);
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

    public override void TakeDamage(float damage)
    {
        if (currentHealthPoints <= 0) 
        {
            Die();
        }
        currentHealthPoints -= damage;
        damageController.TakeDamage();
    }

    public void Modify(float mod)
    {
        attackSpeedModifier = mod;
        attackRangeModifier = mod;
        attackSpeed *= attackSpeedModifier;
        attackRange *= attackRangeModifier;
    }
}
