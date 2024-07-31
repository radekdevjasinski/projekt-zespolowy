using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;

public class ZombieShield : EnemyBase, OnDamage
{
    [SerializeField] private float attackSpeed = 0.1f;
    [SerializeField] private float interpolation = 0.1f;
    [SerializeField] private float wakeUpCooldown = 5f;
    public PlayerEntityController playerController;
    private Animator animator;
    private bool canAttack = true;
    private bool stunned = false;
    private Knockback knockback;
    private Shield shield;

    protected override void Start()
    {
        base.Start();
        currentHealthPoints = maxHelathPoints;
        animator = GetComponent<Animator>();
        playerController = GameObject.Find("Player").GetComponent<PlayerEntityController>();
        knockback = GetComponent<Knockback>();
        shield = GetComponentInChildren<Shield>();

        interpolation = Random.Range(interpolation - 0.05f, interpolation + 0.05f);
        speed = Random.Range(speed - 0.5f, speed + 0.5f);

        //usun
        LockMovement = false;
        //


    }
    void FixedUpdate()
    {
        if (!LockMovement)
        {
            if (!stunned)
            {
                Move();
                animator.speed = speed;
                speed += Time.fixedDeltaTime * 0.1f;
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

       
        Vector2 currentVelocity = rb.velocity.normalized;
        Vector2 smoothedDirection = Vector2.Lerp(currentVelocity, direction, interpolation).normalized;

        if (rb != null)
        {
            rb.velocity = smoothedDirection * speed;
            animator.SetFloat("moveX", smoothedDirection.x);
        }
    }


    protected override void Attack()
    {
        playerController.dealDamage(damage, -rb.velocity);
        //ApplyKnockback((transform.position - GameObject.Find("Player").transform.position).normalized, knockback.knockbackForceAttacking, knockback.knockbackDurationAttacking);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            if (speed > 2)
            {
                Stun();
            }
        }
    }
    public void Stun()
    {
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        animator.speed = 0;
        speed = 0f;
        animator.Play("Move", 0, 0f);
        animator.Update(0f); // Ensure the animator updates immediately
        shield.shieldActive = false;
        stunned = true;
        StartCoroutine(startRunning());

    }
    IEnumerator startRunning()
    {
        yield return new WaitForSeconds(wakeUpCooldown);
        rb.bodyType = RigidbodyType2D.Dynamic;
        shield.shieldActive = true;
        stunned = false;
        speed = 1f;
    }
    public void hitShield(GameObject gameObject)
    {
        if (!stunned)
        {
            Destroy(gameObject);
        }
    }
    public void ShieldBash()
    {
        if (canAttack && !stunned)
        {
            Attack();
            canAttack = false;
            StartCoroutine(attackCooldown());
        }
    }

    protected override void onDie()
    {
        base.onDie();
    }
    public void onDamage()
    {
    }
}