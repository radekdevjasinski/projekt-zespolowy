using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector2 direction;
    public bool active = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && active)
        {
            GameObject.Find("Player").GetComponent<PlayerTeleporter>().Teleport(direction);
        }
    }
}
