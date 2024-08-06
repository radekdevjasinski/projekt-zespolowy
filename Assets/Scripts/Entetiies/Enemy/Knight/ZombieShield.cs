using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;

public class ZombieShield : EnemyBase, OnDamage
{
    [Header("Movement and Attacking")]
    [SerializeField] private float attackSpeed = 0.1f;
    [SerializeField] private float startSpeed = 1f;
    [SerializeField] private float startInterpolation = 0.1f;
    private float interpolation;
    [SerializeField] private float speedUpMultiplier = 0.1f;
    [SerializeField] private float interpolationMultiplier = 0.001f;
    [SerializeField] private float wakeUpCooldown = 5f;
    [SerializeField] private float wallHitLimit = 2f;
    private PlayerEntityController playerController;
    private Animator animator;
    private bool canAttack = true;
    private bool stunned = false;
    private Shield shield;
    private GameObject stars;

    protected override void Start()
    {
        base.Start();
        currentHealthPoints = maxHelathPoints;
        animator = GetComponent<Animator>();
        playerController = GameObject.Find("Player").GetComponent<PlayerEntityController>();
        shield = GetComponentInChildren<Shield>();
        stars = transform.GetChild(1).gameObject;
        stars.SetActive(stunned);
        speed = startSpeed;
        interpolation = startInterpolation;
    }
    void FixedUpdate()
    {
        if (!LockMovement)
        {
            if (!stunned)
            {
                Move();
                animator.speed = speed;
                speed += Time.fixedDeltaTime * speedUpMultiplier;
                interpolation -= Time.fixedDeltaTime * interpolationMultiplier;
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
        if (!collision.collider.CompareTag("Player") && !collision.collider.CompareTag("Enemy") && !collision.collider.CompareTag("Shield") && !collision.collider.CompareTag("Projectile"))
        {
            if (speed > wallHitLimit)
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
        stars.SetActive(stunned);
        CameraController.Instance.Shake();
        StartCoroutine(startRunning());

    }
    IEnumerator startRunning()
    {
        yield return new WaitForSeconds(wakeUpCooldown);
        rb.bodyType = RigidbodyType2D.Dynamic;
        shield.shieldActive = true;
        stunned = false;
        stars.SetActive(stunned);
        speed = startSpeed;
        interpolation = startInterpolation;
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