using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnDeath : MonoBehaviour, DeathSeuance
{
    [SerializeField] private GameObject soundToPlay;

    public void onDeath()
    {
        SoundManager.instance.playSound(transform.parent,soundToPlay,this.transform.position);
    }
}
