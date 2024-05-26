using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoopSoundControls : ConstantSoundController
{

    private bool isPlaying=false;

    public override void playSound()
    {
        isPlaying=true;
        playRandom();
    }


    private void playRandom()
    {
        if (isPlaying)
        {
            AudioSource source = sources[Random.Range(0, sources.Length)];
            source.loop = false;
            source.Play();
            Invoke("playRandom", source.clip.length);
        }
    }

    public override void stopSound()
    {
        isPlaying = false;
        foreach (var source in sources)
        {
            source.Stop();
        }
    }

}
