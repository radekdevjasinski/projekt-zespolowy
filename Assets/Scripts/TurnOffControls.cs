using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffControls : MonoBehaviour
{
    public bool controlsOn = true;

    [Header("Scripts To TurnOff")]
    public PlayerControler playerControler;
    public Animator animator;
    void Start()
    {
        playerControler = GetComponent<PlayerControler>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        playerControler.enabled = controlsOn;
        animator.enabled = controlsOn;
    }
}
