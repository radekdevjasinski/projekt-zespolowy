using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{

    private Controls controls;


    //Controllers



    //Actions
    private InputAction move;
    private InputAction shoot;
    private InputAction useActivatedItem;
    private InputAction useSingleItem;
    private InputAction put;
    private InputAction dropEquiped;

    private void Awake()
    {
        Debug.Log("Awake");
        this.controls =new Controls();
    }

    void OnEnable()
    {
        Debug.Log("OnEnable");
        this.controls.Player.Enable();

        move = this.controls.Player.Move;
        move.performed += ctx => { OnMove(ctx.ReadValue<Vector2>()); };
        move.canceled += ctx => { OnCancelMove(ctx.ReadValue<Vector2>()); };


        shoot = this.controls.Player.Shoot;
        shoot.performed += ctx => { OnShoot(ctx.ReadValue<Vector2>()); };
        shoot.canceled += ctx => { OnCancelShoot(ctx.ReadValue<Vector2>()); };

        useActivatedItem=this.controls.Player.UseActivatedItem;
        useActivatedItem.performed += ctx => { OnUseActivatedItem(); };
        useActivatedItem.canceled += ctx => { OnCancelUseActivatedItem(); };

        useSingleItem = this.controls.Player.UseSingleItem;
        useSingleItem.performed += ctx => { OnUseSingleItem(); };
        useSingleItem.canceled += ctx => { OnCancelUseSingleItem(); };

        put = this.controls.Player.Put;
        put.performed += ctx => { OnPut(); };
        put.canceled += ctx => { OnCancelPut(); };

        dropEquiped = this.controls.Player.DropEquiped;
        dropEquiped.performed += ctx => { OnDropEquiped(); };
        dropEquiped.canceled += ctx => { OnCancelDropEquiped(); };
    }
    void OnDisable()
    {
        this.controls.Player.Disable();
    }

    //On actions
    void OnMove(Vector2 movement)
    {
        Debug.Log("movemnet: " + movement);
    }
    void OnCancelMove(Vector2 movement)
    {
        Debug.Log("cancel movemnet: " + movement);
    }

    void OnShoot(Vector2 movement)
    {
        Debug.Log("Shoot: " + movement);
    }
    void OnCancelShoot(Vector2 movement)
    {
        Debug.Log("cancel Shoot: " + movement);
    }

    void OnUseActivatedItem()
    {
        Debug.Log("UseActivatedItem" );
    }
    void OnCancelUseActivatedItem()
    {
        Debug.Log("CancelActivatedItem");
    }
    void OnUseSingleItem()
    {
        Debug.Log("UseSingleItem");
    }
    void OnCancelUseSingleItem()
    {
        Debug.Log("CancelSingleItem");
    }
    void OnPut()
    {
        Debug.Log("On put");
    }
    void OnCancelPut()
    {
        Debug.Log("Cancel On put");
    }
    void OnDropEquiped()
    {
        Debug.Log("DropEquiped");
    }
    void OnCancelDropEquiped()
    {
        Debug.Log("Cancel DropEquiped");
    }

}
