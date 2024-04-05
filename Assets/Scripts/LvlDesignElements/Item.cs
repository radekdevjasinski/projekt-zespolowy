using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Item : MonoBehaviour
{
    private bool isPlayerOnItem = false;

    void Update()
    {
        if (isPlayerOnItem && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnItem = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnItem = false;
        }
    }
}
