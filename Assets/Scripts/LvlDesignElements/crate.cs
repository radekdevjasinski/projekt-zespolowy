using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class crate : MonoBehaviour, IActivatableObject
{
    [SerializeField] private GameObject openCrateSound;
    private Animator Animator;
    private bool _isOpened=false;

    private void Awake()
    {
        Animator = transform.parent.GetComponent<Animator>();
    }
    public void openCrate()
    {
        if (openCrateSound != null)
        {
            SoundManager.instance.playSound(transform,openCrateSound);
        }

        Animator.SetTrigger("Open");
    }

    public bool activate(GameObject activator)
    {
        openCrate();
        _isOpened=true;
        return true;
    }

    public bool isActivated()
    {
        return _isOpened;
    }
}
