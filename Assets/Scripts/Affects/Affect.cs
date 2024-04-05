using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Affect : MonoBehaviour
{
    [SerializeField] private float timeOfAffect;
    [SerializeField] private GameObject soundOfAffect;

    private GameObject loopSoundInstance;
    private void Start()
    {
        affect();
        if (soundOfAffect != null)
        {
            loopSoundInstance=SoundManager.instance.playLoop(this.transform, soundOfAffect);
            
        }
        Invoke("stop",timeOfAffect);
    }

    public void stop()
    {
        deAffcet();
        if (soundOfAffect != null)
        {
            Destroy(loopSoundInstance);
        }

        Destroy(this.gameObject);
    }

    protected abstract void affect();
    protected abstract void deAffcet();
}
