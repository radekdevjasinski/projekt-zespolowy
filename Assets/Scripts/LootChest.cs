using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChest : ActivatableObject
{
    [SerializeField] public List <GameObject> loot;
    private GameObject player;
    [SerializeField] private bool isBigChest;
    [SerializeField] private Sprite openChest;
    private SpriteRenderer renderer;
    private bool isOpened = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    protected override void onActivate(GameObject player)
    {
        if (!isOpened)
        {
            if (isBigChest)
            {
                if (player.GetComponent<PlayerItemsController>().getKeys()>=1)
                {
                    isOpened = true;
                    renderer.sprite = openChest;
                    player.GetComponent<PlayerItemsController>().removeKey(1);
                    dropLoot();
                }
            }
            else
            {
                isOpened = true;
                renderer.sprite = openChest;
                dropLoot();
            }
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
