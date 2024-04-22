using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounfOnHit : MonoBehaviour, OnDamage
{
    [SerializeField] private GameObject sound;
    public void onDamage()
    {
        Debug.Log("On damge sound: "+ transform.name);
        SoundManager.instance.playSound(this.transform, this.sound);
    }
}
