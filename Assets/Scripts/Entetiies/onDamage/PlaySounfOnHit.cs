using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounfOnHit : MonoBehaviour, OnDamage
{
    [SerializeField] private GameObject sound;
    public void onDamage()
    {
        SoundManager.instance.playSound(this.transform, this.sound, this.transform.position);
    }
}
