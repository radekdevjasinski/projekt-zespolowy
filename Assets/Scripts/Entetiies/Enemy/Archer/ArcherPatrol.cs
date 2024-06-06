using System.Collections;
using UnityEngine;

public class ArcherPatrol : EnemyBase
{
    private bool facingRight = true;
    private Animator animator;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] public bool isShooting;

    protected override void Start()
    {
        base.Start();
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        animator = GetComponentInChildren<Animator>();
        EnemyBase enemy = GetComponent<EnemyBase>();
    }

    void FixedUpdate()
    {
        if (!LockMovement)
        {
            if (isShooting)
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        animator.SetBool("isShooting", isShooting);
    }

    private void OnCollisionStay2D(Collision2D collision)
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
