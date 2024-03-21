using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float delay = 3f;
    void Start()
    {
        Destroy(gameObject, delay); 
    }
}
