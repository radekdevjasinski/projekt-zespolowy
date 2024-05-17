using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
struct element
{
    [SerializeField] public string name;
    [SerializeField] public GameObject sound;
}
public class PlaySoundController : MonoBehaviour
{

    [SerializeField] private element[] sounds;
    private Dictionary<string, GameObject> soundsDictionary;


    private void Awake()
    {
        soundsDictionary = sounds.ToDictionary(element => element.name, element => element.sound);
        
    }

    public void playSound(string name)
    {
        SoundManager.instance.playSound(transform, soundsDictionary[name]);
    }

}
