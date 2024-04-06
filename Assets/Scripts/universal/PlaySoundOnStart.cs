using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] private GameObject sound;
    [SerializeField] private float timeOffset;

    private void Start()
    {
        Invoke("playSound", timeOffset);
    }

    private void playSound()
    {
        SoundManager.instance.playSound(this.transform,sound,new Vector3(0,0,0));
    }
}
