using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Chest : MonoBehaviour
{
    public GameObject itemPrefab;
    private bool isOpen = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isOpen) 
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOpen = true;
            int quantityItems = Random.Range(3, 5);
            float angleDistance = 360 / quantityItems;
            for (int i = 0; i < quantityItems; i++)
            {
                Vector2 randomPos = RandomCircle(transform.position, 1f, angleDistance, i);

                GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
                item.GetComponent<ItemFall>().startPoint = transform.position;
                item.GetComponent<ItemFall>().endPoint = randomPos;
            }
        }
    }
    private Vector2 RandomCircle(Vector2 center, float radius,float angleDistance, int i)
    {
        float angle = angleDistance * i;
        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad); // Obliczanie wspó³rzêdnej x
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad); // Obliczanie wspó³rzêdnej y
        return pos;
    }
}
