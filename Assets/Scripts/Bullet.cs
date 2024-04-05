using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]float speed = 10.0f;
    [SerializeField]int attack = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (-transform.up) * speed;
        Destroy(gameObject, 2f); 
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHp hp = collision.gameObject.GetComponent<PlayerHp>();
        if (hp != null)
        {
            hp.TakeDamage(attack);
        }
        Destroy(gameObject);
    }
}
