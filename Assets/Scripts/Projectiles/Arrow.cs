using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    int attack = 1;
    public float speed = 5f;

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
