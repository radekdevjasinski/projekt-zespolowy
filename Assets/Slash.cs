using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [Header("Animation")]
    public float moveSpeed;
    public float animationSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.velocity = transform.up * moveSpeed;
        animator.speed = animationSpeed;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
