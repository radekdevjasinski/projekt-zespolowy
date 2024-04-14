using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAcrossMapTest : MonoBehaviour
{
    [SerializeField] private GameObject instance;
    [SerializeField] private int amountTOSpawn;
    [SerializeField] private float delyBetwenSpawn;

    private BossFightController bossFightController;
    void Start()
    {
        bossFightController=GetComponentInParent<BossFightController>();
        for (int i = 0; i < amountTOSpawn; i++)
        {
            Invoke("summon",i* delyBetwenSpawn);
        }
    }

    private void summon()
    {
        Vector2 position = bossFightController.getRandomPostionOnBossMap(1);
        Debug.Log("summon at positon: "+ position);
        Instantiate(instance, new Vector3(position.x,position.y),new Quaternion(),transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
