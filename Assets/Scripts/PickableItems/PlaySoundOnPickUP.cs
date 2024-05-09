using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnPickUP : MonoBehaviour, IonItemPickUP
{
    [SerializeField] private GameObject onItemPickSound;
    public void onItemPicked(GameObject gameobject)
    {
        if (onItemPickSound != null)
            SoundManager.instance.playSound(onItemPickSound, this.transform.position);
    }

    internal void setAudio(GameObject soundOnBuy)
    {
        onItemPickSound=soundOnBuy;
    }
}
