using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChest : ActivatableObject
{
    [SerializeField] public GameObject[] loot;
    private bool isOpened = false;
    protected override void onActivate(GameObject player)
    {
        if (!isOpened)
        {
            isOpened=true;
            Debug.Log("chest open");
            dropLoot();
        }
    }

    void pushItem(GameObject obj)
    {
        GameObject loot= Instantiate(
            obj,
            this.transform.position,
            new Quaternion(0, 0, 0,0)
        );
        float roatation = Random.Range(0,2*Mathf.PI);
        Vector2 push = new Vector2(Mathf.Sin(roatation), Mathf.Cos(roatation));
        loot.GetComponent<Rigidbody2D>().AddForce(push * 3,ForceMode2D.Impulse);
        
        
    }

    private void dropLoot()
    {
        foreach (GameObject var in loot)
        {
            pushItem(var);
        }
    }
}
