using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MethodAtStart : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;

    public void Start()
    {
        this._event.Invoke();
    }
}
