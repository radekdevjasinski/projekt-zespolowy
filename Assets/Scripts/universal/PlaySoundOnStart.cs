using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] private GameObject sound;
    [SerializeField] private float timeOffset;
    [SerializeField] private bool thisAsParent = true;

    private void Start()
    {
        Invoke("playSound", timeOffset);
    }

    private void playSound()
    {
        if(thisAsParent)
        SoundManager.instance.playSound(this.transform,sound,new Vector3(0,0,0));
        else
        {
            SoundManager.instance.playSound(this.sound, transform.position);
        }
    }
}
