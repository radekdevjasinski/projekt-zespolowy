using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SoftwareCatcherPlayerController : MonoBehaviour, IplayerControllerInterface
{
    private Controls controls;


    //Controllers
    SoftwareCatcherPlayerMovement softwareCatcherPlayerMovement;


    //Actions
    private InputAction move;



    private void Awake()
    {

        this.controls = new Controls();
        this.softwareCatcherPlayerMovement = this.GetComponent<SoftwareCatcherPlayerMovement>();

    }

    void OnEnable()
    {
        this.controls.Player.Enable();

        move = this.controls.Player.Move;
        move.performed += ctx => { OnMove(ctx.ReadValue<Vector2>()); };
        move.canceled += ctx => { OnCancelMove(ctx.ReadValue<Vector2>()); };


     


    }


    void OnDisable()
    {
        this.controls.Player.Disable();
    }

    //On actions
    void OnMove(Vector2 movement)
    {
        softwareCatcherPlayerMovement.setMoveValue(movement);
    }
    void OnCancelMove(Vector2 movement)
    {
        softwareCatcherPlayerMovement.setMoveValue(movement);
    }


    public void unlockInput()
    {
        controls.Enable();
    }

    public void lockInput()
    {
        controls.Disable();
    }
}
