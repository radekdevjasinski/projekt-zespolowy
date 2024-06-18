using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogOnAwakeTestScipt : MonoBehaviour
{
    [SerializeField] private String messeage;

    void Awake()
    {
        Debug.Log("log test: "+ messeage);
    }
}
