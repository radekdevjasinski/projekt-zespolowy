using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtHeartBar : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerAttributesController playerAttributesController;
    List<HealthHeart> hearts = new List<HealthHeart>();

    private int maxHealth;
    private int health;

    private void Start()
    {
        maxHealth = playerAttributesController.Health;
        health = maxHealth;
        DrawHearts();
    }
    public void DrawHearts()
    {
        Debug.Log("drawign heats: "+ playerAttributesController.Health);
        health = playerAttributesController.Health;
        ClearHearts();
        int maxHealthReminder = maxHealth % 2;
        int HeartsToMake = (maxHealth / 2 + maxHealthReminder);
        for(int i = 0;i<HeartsToMake;i++)
        {
            CreateEmptyHeart();
        }

        for(int i = 0; i<hearts.Count;i++) 
        {
            int heartStatusReminder = Mathf.Clamp(health - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusReminder);
        }
    }
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);
        newHeart.transform.localScale = new Vector3(1, 1, 0);
        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }
    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }
}
