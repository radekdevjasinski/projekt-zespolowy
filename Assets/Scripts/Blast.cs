using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float delay = 3f;
    [SerializeField] private GameObject blastSound;
    void Start()
    {
        SoundManager.instance.playSound(blastSound,transform.position);
        Destroy(gameObject, delay); 
    }
}
