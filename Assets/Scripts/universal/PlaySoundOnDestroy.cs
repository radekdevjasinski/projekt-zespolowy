using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnDestroy : MonoBehaviour
{

    [SerializeField] private GameObject sound;

    private void OnDestroy()
    {
        if(SoundManager.instance!=null)
        SoundManager.instance.playSound(sound,transform.position);
    }
}
