
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance { get; private set; }

    [SerializeField]
    private GameObject BasicAmbientSounds;
    public GameObject ActiveAmbient;
    private Transform ambientParent;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("TWo instances of sound manager");
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        ambientParent = transform.Find("Ambient").transform;
        setAmbient(BasicAmbientSounds);
    }
    public void playSound(GameObject soundObject, Vector3 postion)
    {
        GameObject soundGameObjet = playSound(this.transform, soundObject, postion);
        AudioSource audio = soundGameObjet.GetComponent<AudioSource>();
        //Debug.Log("play sound: " + soundObject.GetComponent<AudioSource>().clip.name + " ini postion: "+ postion);
DestroyAfterTime.Destroy(soundGameObjet, audio.clip.length);
    }
    public GameObject playSound(Transform parent, GameObject soundObject, Vector3 postion)
    {
        GameObject soundGameObjet = Instantiate(soundObject, new Vector3(postion.x,postion.y,0), new Quaternion(0, 0, 0, 0), parent);
        //Debug.Log("play sound: " + soundGameObjet.GetComponent<AudioSource>().clip.name+ " at positon: "+ soundGameObjet.transform.position);
        AudioSource audio = soundGameObjet.GetComponent<AudioSource>();
        //DestroyAfterTime.Destroy(soundGameObjet, audio.clip.length);
        return soundGameObjet;
    }

    public void playSound(Transform parent, GameObject soundObject)
    {
        GameObject soundGameObjet = Instantiate(soundObject, parent.position, new Quaternion(0, 0, 0, 0), parent);
        //Debug.Log("play sound: " + soundGameObjet.GetComponent<AudioSource>().clip.name);
        AudioSource audio = soundGameObjet.GetComponent<AudioSource>();
        DestroyAfterTime.Destroy(soundGameObjet, audio.clip.length);
    }
    public GameObject playLoop(Transform parent, GameObject loopSOundPrefab)
    {
        GameObject soundGameObjet = Instantiate(loopSOundPrefab, parent.position, new Quaternion(0, 0, 0, 0), parent);
        //Debug.Log("play kkloop sound: " + soundGameObjet.GetComponent<AudioSource>().clip.name);
        AudioSource audio = soundGameObjet.GetComponent<AudioSource>();
        audio.Play();
        return soundGameObjet;
    }

    public void setAmbient(GameObject ambientPack)
    {

        Debug.Log("set ambient: "+ ambientPack.name);
        if (this.ActiveAmbient != null)
        {
            Destroy(this.ActiveAmbient);
        }

        ActiveAmbient = Instantiate(ambientPack, this.ambientParent);
    }

    public void revertToBasicAmbient()
    {
        if (this.ActiveAmbient.gameObject != this.BasicAmbientSounds)
        {
            setAmbient(this.BasicAmbientSounds);
        }
    }

    internal RandomLoopSoundControls playRandomLoop(Transform transform, GameObject mumblePack)
    {
        GameObject soundObject= Instantiate(mumblePack,transform);
      return soundObject.AddComponent<RandomLoopSoundControls>();
    }
}