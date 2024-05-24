using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : EnemyBase, OnDamage
{
    [SerializeField] private float attackSpeed = 0;
    [SerializeField] private float attackRange = 1.5f;
    public PlayerEntityController playerController;
    public float attackSpeedModifier = 1;
    public float attackRangeModifier = 1;
    private Animator animator;
    private KnightPathfinding knightPathfinding;
    private bool canAttack = true;
    private bool isInRange = false;
    private Knockback knockback;

    protected override void Start()
    {
        base.Start();
        currentHealthPoints = maxHelathPoints;
        damage = 1;
        animator = GetComponent<Animator>();
        knightPathfinding = GetComponent<KnightPathfinding>();
        knightPathfinding.agent.speed = this.speed;
        playerController = GameObject.Find("Player").GetComponent<PlayerEntityController>();
        knockback = GetComponent<Knockback>();
    }
    void FixedUpdate()
    {
        /*if (!LockMovement)
        {
            if (!IsWithinRange())
            {
                Move();
            }
            if (IsWithinRange() & canAttack)
            {
                Attack();
                //animator.SetTrigger("isAttacking");
                canAttack = false;
                StartCoroutine(attackCooldown());

            }
        }*/
        if (!LockMovement)
        {
            Move();
            if (isInRange & canAttack)
            {
                Attack();
                //animator.SetTrigger("isAttacking");
                canAttack = false;
                StartCoroutine(attackCooldown());
            }
        }
    }

    public void ApplyKnockback(Vector2 direction, float knockbackForce, float knockbackDuration)
    {
        knockback?.ApplyKnockback(direction, knockbackForce, knockbackDuration);
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
        /*Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                playerController.dealDamage(damage);
                ApplyKnockback((transform.position - collider.transform.position).normalized, knockback.knockbackForceAttacking, knockback.knockbackDurationAttacking);
                Debug.Log("Hit Player and applied knockback");
            }
        }*/
        playerController.dealDamage(damage);
        ApplyKnockback((transform.position - GameObject.Find("Player").transform.position).normalized, knockback.knockbackForceAttacking, knockback.knockbackDurationAttacking);
        //Debug.Log("Hit Player and applied knockback");
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
        attackSpeedModifier = mod;
        attackRangeModifier = mod;
        attackSpeed *= attackSpeedModifier;
        attackRange *= attackRangeModifier;
    }

    public void onDamage()
    {
        ApplyKnockback((transform.position - playerController.gameObject.transform.position).normalized, knockback.knockbackForceAttacked, knockback.knockbackDurationAttacked);
    }
}