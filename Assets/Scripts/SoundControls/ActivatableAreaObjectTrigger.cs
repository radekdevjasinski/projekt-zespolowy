using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ActivatableAreaObjectTrigger : MonoBehaviour, IActivatableObject
{

    [SerializeField] private GameObject onAcvtivationSound;
    private bool _isActivated = false;
    [SerializeField] bool mulitiActivatable=false;
    private void OnTriggerEnter2D (Collider2D collider)
    {
        Debug.Log("Collsion: "+ collider.tag);
        if (collider.CompareTag("Player") && (!isActivated() || mulitiActivatable))
        {
            Debug.Log("correct collsion: " + collider.tag);

            if (activate(collider.gameObject) && onAcvtivationSound != null)
            {
                SoundManager.instance.playSound(transform, onAcvtivationSound);
                _isActivated = true;
            }

            ;
            
        }
    }

    public abstract bool activate(GameObject player);

    public bool isActivated()
    {
        return _isActivated;
    }
}
