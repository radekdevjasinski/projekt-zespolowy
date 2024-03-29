using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float delay = 3f;
    [SerializeField] private AudioClip blastSound;
    void Start()
    {
        SoundManager.playSound(blastSound);
        Destroy(gameObject, delay); 
    }
}
