using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void playSound( GameObject soundObject, Vector3 postion)
    {
        playSound(this.transform, soundObject, postion);
    }
    public void playSound(Transform parent,GameObject soundObject, Vector3 postion)
    {
        GameObject soundGameObjet = Instantiate(soundObject, postion, new Quaternion(0, 0, 0, 0), parent);
        //Debug.Log("play sound: " + soundGameObjet.GetComponent<AudioSource>().clip.name);
        AudioSource audio = soundGameObjet.GetComponent<AudioSource>();
        DestroyAfterTime.Destroy(soundGameObjet, audio.clip.length);
    }

    public GameObject playLoop(Transform parent, GameObject loopSOundPrefab)
    {
        GameObject soundGameObjet = Instantiate(loopSOundPrefab, parent.position, new Quaternion(0, 0, 0, 0), parent);
        //Debug.Log("play loop sound: " + soundGameObjet.GetComponent<AudioSource>().clip.name);
        AudioSource audio = soundGameObjet.GetComponent<AudioSource>();
        audio.Play();
        return soundGameObjet;
    }
}
