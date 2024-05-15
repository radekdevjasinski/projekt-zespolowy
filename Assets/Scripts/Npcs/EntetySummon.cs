using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntetySummon : MonoBehaviour
{
    [SerializeField] private bool summonOnStart;

    [SerializeField] private GameObject objectToSummon;

    private void Start()
    {
        if(summonOnStart)
            summon();
    }

    public void summon()
    {
        Instantiate(objectToSummon, transform);
    }

}
