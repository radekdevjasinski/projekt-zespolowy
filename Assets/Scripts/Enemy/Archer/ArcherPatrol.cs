using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ArcherPatrol : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool facingRight = true;
    [SerializeField]private float moveSpeed = 1f; // Prêdkoœæ poruszania siê

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y); // Ustawienie sk³adowej Y na zero
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            FlipSprite();
            ChangeDirection();
        }
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
        moveSpeed *= -1; // Zmiana kierunku ruchu przez zmianê znaku prêdkoœci
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y); // Ustawienie nowej prêdkoœci
    }
}
