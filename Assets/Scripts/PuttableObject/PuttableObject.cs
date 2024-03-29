using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PuttableObject : MonoBehaviour
{
    [SerializeField] private GameObject onPutSound;
    [SerializeField] private GameObject Prefab;

    private void Start()
    {
        if(onPutSound!=null)
            SoundManager.instance.playSound(onPutSound,transform.position);
        onPut();
    }

    protected abstract void onPut();


}
