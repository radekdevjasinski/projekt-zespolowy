using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBar : MonoBehaviour
{
    public GameObject armorPrefab;
    public PlayerAttributesController playerAttributesController;
    List<GameObject> armors = new List<GameObject>();

    private int armor;

    private void Start()
    {
        armor = playerAttributesController.Armor;
        DrawArmor();
    }
    public void DrawArmor()
    {
        armor = playerAttributesController.Armor;
        ClearArmor();
        for(int i = 0; i < armor; i++)
        {
            DrawSingleArmor();
        }
    }
    public void DrawSingleArmor()
    {
        GameObject newArmor = Instantiate(armorPrefab);
        newArmor.transform.SetParent(transform);
        newArmor.transform.localScale = new Vector3(1, 1, 0);
        armors.Add(newArmor);
    }
    public void ClearArmor()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        armors = new List<GameObject>();
    }
}
