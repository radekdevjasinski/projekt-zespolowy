using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichSummonController : MonoBehaviour
{
    [Header("Effect")]
    [SerializeField] private GameObject appearEffect;
    void Start()
    {
        Debug.Log("apepar lich");
        if (appearEffect != null)
            Instantiate(appearEffect, this.transform, false);
    }

    
 
}
