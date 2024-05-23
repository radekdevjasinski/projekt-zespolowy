using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLastHitted : MonoBehaviour
{
    [SerializeField] private GameObject lastTouchedObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        lastTouchedObject = collision.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        lastTouchedObject = other.gameObject;
    }

    public GameObject GetLastTouchedObject()
    {
        return lastTouchedObject;
    }
}
