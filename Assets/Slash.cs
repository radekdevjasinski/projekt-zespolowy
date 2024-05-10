using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Projectile
{
    [Header("Animation")]
    public float moveSpeed;
    public float animationSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    PlayerAttributesController playerAttributesController;
    void Start()
    {
        playerAttributesController = GameObject.Find("Player").GetComponent<PlayerAttributesController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.velocity = transform.up * (moveSpeed * playerAttributesController.Range);
        animator.speed = animationSpeed;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    protected override void onHit(GameObject obj)
    {
        EntityController<float> entityController;
        if (obj.TryGetComponent<EntityController<float>>(out entityController))
        {
            
            entityController.dealDamage(playerAttributesController.Damage);
        }
    }

    protected override void OnDestroyed()
    {
        //throw new System.NotImplementedException();
    }
    protected override void kill()
    {
        OnDestroyed();
    }
}
