using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class UsableItem: PicableItem
{
    [SerializeField]
    private GameObject soundOnUse;

    public void use(GameObject obh)
    {
        if (soundOnUse != null)
        {
            SoundManager.instance.playSound(soundOnUse,obh.transform.position);
           
        }
        onUse(obh);
    }

    protected abstract void onUse(GameObject obj);

    public AudioClip getAudioClip()
    {
        return this.soundOnUse.GetComponent<AudioSource>().clip;
    }
}
