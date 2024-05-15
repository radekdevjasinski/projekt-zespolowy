using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ActivatableObject : MonoBehaviour
{

    [SerializeField] private GameObject onAcvtivationSound;

    private void OnCollisionEnter2D(Collision2D collsion)
    {
        Debug.Log("Collsion: "+ collsion.collider.tag);
        if (collsion.collider.CompareTag("Player"))
        {
            if(onAcvtivationSound!=null)
                SoundManager.instance.playSound(transform, onAcvtivationSound);
            onActivate(collsion.collider.gameObject);
        }
    }

    protected abstract void onActivate(GameObject player);

}
