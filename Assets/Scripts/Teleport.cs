using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector2 direction;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("teleport enter");
            GameObject.Find("Player").GetComponent<TeleportToNextRoom>().Teleport(direction);
        }
    }
}
