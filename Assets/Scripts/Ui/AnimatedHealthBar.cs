using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedHealthBar : MonoBehaviour
{
    public GameObject fullHeartPrefab;
    public GameObject halfHeartPrefab;
    public GameObject emptyHeartPrefab;

    public PlayerAttributesController playerAttributesController;

    private void Start()
    {
        playerAttributesController = GameObject.Find("Player").GetComponent<PlayerAttributesController>();
        DrawHearts();
    }
    public void DrawHearts()
    {
        int maxHealth = playerAttributesController.getMaxHealth();
        int health = playerAttributesController.Health;

        Debug.Log($"SERCA: {maxHealth}, {health} ");

        ClearHearts();

        float healthPercentage = (float)health / maxHealth;
        int numHearts = Mathf.CeilToInt(healthPercentage * maxHealth);

        int numFullHearts = numHearts / 2;
        int numHalfHearts = numHearts % 2;
        int numEmptyHearts = (maxHealth / 2) - numFullHearts - numHalfHearts;

        // Debugowanie, aby zobaczyæ obliczenia
        Debug.Log($"MaxHealth: {maxHealth}, Health: {health}");
        Debug.Log($"NumHearts: {numHearts}, FullHearts: {numFullHearts}, HalfHearts: {numHalfHearts}, EmptyHearts: {numEmptyHearts}");

        for (int i = 0;i < numFullHearts; i++)
        {
            CreateHeart(fullHeartPrefab);
        }
        for (int i = 0; i < numHalfHearts; i++)
        {
            CreateHeart(halfHeartPrefab);
        }
        for (int i = 0; i < numEmptyHearts; i++)
        {
            CreateHeart(emptyHeartPrefab);
        }

    }
    public void CreateHeart(GameObject heartPrefab)
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);
        newHeart.transform.localScale = new Vector3(1, 1, 0);
    }
    public void ClearHearts()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
