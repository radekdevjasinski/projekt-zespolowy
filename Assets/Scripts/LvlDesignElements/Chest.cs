using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject itemPrefab;
    private bool isOpen = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isOpen) 
        {
            Debug.Log("Opne");
            if (Input.GetKeyDown(KeyCode.F)) 
            {
                isOpen = true;
                int quantityItems = Random.Range(1, 5);
                float angleDistance = 360 / quantityItems;
                for(int i=0; i<quantityItems; i++)
                {
                    Vector2 randomPos = RandomCircle(transform.position, 1f, angleDistance, i); 
                    Instantiate(itemPrefab, randomPos, Quaternion.identity);
                    //GameObject newItem = Instantiate(itemPrefab, new Vector2(transform.position.x - 2f+i, transform.position.y + 1f), Quaternion.identity);
                }
            }
        }
    }
    private Vector2 RandomCircle(Vector2 center, float radius,float angleDistance, int i)
    {

        //float angle = Random.Range(0f, 360f); // Losowy k¹t w stopniach
        float angle = angleDistance * i;
        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad); // Obliczanie wspó³rzêdnej x
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad); // Obliczanie wspó³rzêdnej y
        return pos;
    }
}
