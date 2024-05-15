using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class DiradSpawnPointOnEntry : MonoBehaviour, IOnFirstEntryInRoom
{
    [SerializeField]
    [Range(0, 1)]
    private float appearChance;

    [SerializeField] private Transform driadLocation;
    [SerializeField] private GameObject driadPrefab;

    public void onFirstEntry(Vector2Int roomPositon)
    {
        if (Random.value < appearChance)
        {
            GameObject obj= Instantiate(driadPrefab, driadLocation);
            gameObject.AddComponent<ActionOnRoomClered>().setup(() =>
            {
                obj.GetComponent<EntetySummon>().summon();
            });
          
        }
    }
}
