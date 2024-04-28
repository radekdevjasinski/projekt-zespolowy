using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSoundController : MonoBehaviour
{


    protected AudioSource[] sources;

    private void Start()
    {
        sources = GetComponentsInChildren<AudioSource>();
    }


    public virtual void playSound()
    {
        foreach (var source in sources) {
            source.loop = true;
            source.Play();
                }
    }


    public virtual void stopSound()
    {
        foreach (var source in sources)
        {
            source.Stop();
        }
    }

    public virtual void remove()
    {
        Destroy(gameObject);
    }
}
