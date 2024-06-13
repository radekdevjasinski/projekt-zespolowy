using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    [SerializeField]
    private GameObject BasicAmbientSounds;
    public GameObject ActiveAmbient;
    private Transform ambientParent;

    public float MusicVolume { get; set; }
    public bool IsMuted { get; set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Two instances of SoundManager");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        ambientParent = transform.Find("Ambient").transform;
        setAmbient(BasicAmbientSounds);

        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        IsMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;

        ApplySettings();
    }

    private void ApplySettings()
    {
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>(true);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = MusicVolume;
            audioSource.mute = IsMuted;
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetInt("IsMuted", IsMuted ? 1 : 0);
    }

    public void StopAllSounds()
    {
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>(true);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
        }
    }

    public void setAmbient(GameObject ambientPack)
    {
        Debug.Log("set ambient: " + ambientPack.name);
        if (this.ActiveAmbient != null)
        {
            Destroy(this.ActiveAmbient);
        }

        ActiveAmbient = Instantiate(ambientPack, this.ambientParent);
        ApplySettings();
    }

    public void revertToBasicAmbient()
    {
        if (this.ActiveAmbient.gameObject != this.BasicAmbientSounds)
        {
            setAmbient(this.BasicAmbientSounds);
        }
    }

    public void playSound(GameObject soundObject, Vector3 position)
    {
        GameObject soundGameObject = playSound(this.transform, soundObject, position);
        AudioSource audio = soundGameObject.GetComponent<AudioSource>();
        DestroyAfterTime.Destroy(soundGameObject, audio.clip.length);
    }

    public GameObject playSound(Transform parent, GameObject soundObject, Vector3 position)
    {
        GameObject soundGameObject = Instantiate(soundObject, new Vector3(position.x, position.y, 0), Quaternion.identity, parent);
        return soundGameObject;
    }

    public void playSound(Transform parent, GameObject soundObject)
    {
        GameObject soundGameObject = Instantiate(soundObject, parent.position, Quaternion.identity, parent);
        DestroyAfterTime.Destroy(soundGameObject, soundGameObject.GetComponent<AudioSource>().clip.length);
    }

    public GameObject playLoop(Transform parent, GameObject loopSoundPrefab)
    {
        GameObject soundGameObject = Instantiate(loopSoundPrefab, parent.position, Quaternion.identity, parent);
        soundGameObject.GetComponent<AudioSource>().Play();
        return soundGameObject;
    }

    internal RandomLoopSoundControls playRandomLoop(Transform transform, GameObject mumblePack)
    {
        GameObject soundObject = Instantiate(mumblePack, transform);
        return soundObject.AddComponent<RandomLoopSoundControls>();
    }
}
