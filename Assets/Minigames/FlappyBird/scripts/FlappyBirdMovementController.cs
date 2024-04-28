using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdMovementController : MonoBehaviour
{
    [SerializeField]
    float jumpForce = 5f;
    Rigidbody2D rb;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    public void jump()
    {
        rb.velocity = new Vector2 (0, jumpForce);
        
    }
}
