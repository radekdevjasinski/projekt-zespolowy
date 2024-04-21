using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 5f;
    int attack = 1; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 2f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerEntityController playerController = collision.gameObject.GetComponent<PlayerEntityController>();
            if (playerController)
            {
                playerController.dealDamage(attack);
            }
        }
        Destroy(gameObject);
    }

}
