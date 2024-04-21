using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

    protected bool isGrounded;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void CollisionChecks()
    {
        isGrounded = false;
    }
}
