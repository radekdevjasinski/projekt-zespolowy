
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance { get; private set; }

    [SerializeField]
    private GameObject BasicAmbientSounds;
    private GameObject ActiveAmbient;
    private Transform ambientParent;

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

        ambientParent = transform.Find("Ambient").transform;
        setAmbient(BasicAmbientSounds);
    }
    public void playSound( GameObject soundObject, Vector3 postion)
    {
        playSound(this.transform, soundObject, postion);
       //Debug.Log("play sound: " + soundObject.GetComponent<AudioSource>().clip.name);
    }
    public void playSound(Transform parent,GameObject soundObject, Vector3 postion)
    {
        GameObject soundGameObjet = Instantiate(soundObject, postion, new Quaternion(0, 0, 0, 0), parent);
        Debug.Log("play sound: " + soundGameObjet.GetComponent<AudioSource>().clip.name);
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
}
