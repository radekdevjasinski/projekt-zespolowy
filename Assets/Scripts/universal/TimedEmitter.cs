using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEmitter : MonoBehaviour
{
    [SerializeField] private GameObject toEmit;
    [SerializeField] private float delay;
    void Start()
    {
        emit();
    }

    private void emit()
    {
        Instantiate(toEmit, transform);
        Invoke("emit", delay);
    }
}
