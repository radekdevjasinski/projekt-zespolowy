using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFlappyBirdControler : MonoBehaviour, IplayerControllerInterface
{

    private Controls controls;
    private FlappyBirdMovementController movementController;


    //Actions
    private InputAction jump;


    private void Awake()
    {

        this.controls = new Controls();
        movementController=GetComponent<FlappyBirdMovementController>();
    }

    void OnEnable()
    {
        this.controls.Player.Enable();

        jump = this.controls.Player.UseActivatedItem;
        jump.performed += ctx => { onJump(); };
    }

    private void onJump()
    {
        //Debug.Log("JUMP");
        movementController.jump();
    }

    public void lockInput()
    {
        controls.Disable();
    }

    public void unlockInput()
    {
        controls.Enable();
    }
}