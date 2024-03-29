using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PuttableObject : MonoBehaviour
{
    [SerializeField] private AudioClip onPutSound;
    [SerializeField] private GameObject Prefab;

    private void Start()
    {
        if(onPutSound!=null)
            SoundManager.playSound(onPutSound);
        onPut();
    }

    protected abstract void onPut();


}
