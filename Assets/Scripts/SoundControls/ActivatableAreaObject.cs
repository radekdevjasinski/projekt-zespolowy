using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ActivatableAreaObject : MonoBehaviour, IActivatableObject
{

    [SerializeField] private GameObject onAcvtivationSound;
    private bool _isActivated = false;
    private void OnCollisionEnter2D(Collision2D collsion)
    {
        Debug.Log("Collsion: "+ collsion.collider.tag);
        if (collsion.collider.CompareTag("Player") && !isActivated())
        {
            if (onAcvtivationSound != null && activate(collsion.collider.gameObject))
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
