using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtHeartBar : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerHp playerHealth;
    List<HealthHeart> hearts = new List<HealthHeart>();

    private void OnEnable()
    {
        PlayerHp.OnPlayerDamaged += DrawHearts;
    }
    private void OnDisable()
    {
        PlayerHp.OnPlayerDamaged -= DrawHearts;
    }

    private void Start()
    {
        DrawHearts();
    }
    public void DrawHearts()
    {
        ClearHearts();
        int maxHealthReminder = playerHealth.maxHp % 2;
        int HeartsToMake = (playerHealth.maxHp / 2 + maxHealthReminder);
        for(int i = 0;i<HeartsToMake;i++)
        {
            CreateEmptyHeart();
        }

        for(int i = 0; i<hearts.Count;i++) 
        {
            int heartStatusReminder = Mathf.Clamp(playerHealth.hp - (i * 2), 0, 2);
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
