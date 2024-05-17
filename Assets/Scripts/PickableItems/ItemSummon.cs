using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSummon : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsToSummon;
    [SerializeField] private GameObject soundOnSummon;
    [SerializeField] private bool summonOnStart=false;

    private void Start()
    {
        if(summonOnStart)
            summon();
    }
    public void summon()
    {
        GameObject itemToSummon = itemsToSummon[Random.Range(0, itemsToSummon.Length)];
        GameObject obj= Instantiate(itemToSummon,transform);
        if (soundOnSummon != null)
        {
            SoundManager.instance.playSound(transform, soundOnSummon);
        }
       
    }

    
}
