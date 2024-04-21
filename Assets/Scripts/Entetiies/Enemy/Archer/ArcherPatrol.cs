using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ArcherPatrol : EnemyBase
{
    private Rigidbody2D rb;
    private bool facingRight = true;
    private Animator animator;
    [SerializeField]private float moveSpeed = 1f;

    [SerializeField]private bool isShooting;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        animator = GetComponentInChildren<Animator>();
        EnemyBase enemy = GetComponent<EnemyBase>();
    }

    void Update()
    {
        animator.SetBool("isShooting", isShooting);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            FlipSprite();
            ChangeDirection();
        }
    }
    public void SetVelocityToZero()
    {
        rb.velocity = Vector2.zero;
        isShooting = true;
        
    }
    public void SetVelocity()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        isShooting = false;

    }

    private void FlipSprite()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void ChangeDirection()
    {
        moveSpeed *= -1;
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }


}