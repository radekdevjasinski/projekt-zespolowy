using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static 
    class SoundManager 
{
    public static void playSound(AudioClip clip)
    {
        GameObject soundGameObjet = new GameObject();
        AudioSource audio= soundGameObjet.AddComponent<AudioSource>();
        audio.PlayOneShot(clip);
        DestroyAfterTime.Destroy(soundGameObjet, clip.length);
    }
}
