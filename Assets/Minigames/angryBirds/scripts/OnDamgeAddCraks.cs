using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDamgeAddCraks : MonoBehaviour, OnDamage
{
    [SerializeField] private GameObject cracksPrefab;

    private Transform cracksParent;
    private EntityController<int> entity;


    private void Awake()
    {
        cracksParent = transform.Find("cracks").GetComponent<Transform>();
        entity = GetComponent < EntityController<int>>();
    }


    public void onDamage()
    {
        GameObject cracks= Instantiate(cracksPrefab, cracksParent);
        cracks.transform.Rotate(new Vector3(0,0,1),90* entity.getHealth());
    }
}
