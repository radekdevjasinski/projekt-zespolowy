using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{

    private Controls controls;


    //Controllers
    PlayerMovementController playerMovementController;
    PlayerActionsController playerActionsController;
    PlayerAttributesController playerAttributesController;


    //Actions
    private InputAction move;
    private InputAction shoot;
    private InputAction useActivatedItem;
    private InputAction useSingleItem;
    private InputAction put;
    private InputAction dropEquiped;
    private InputAction mousePress;
    private InputAction mousePosition;
    private InputAction useHealthPotion;


    private void Awake()
    {

        this.controls = new Controls();
        this.playerMovementController = this.GetComponent<PlayerMovementController>();
        this.playerActionsController = this.GetComponent<PlayerActionsController>();
        this.playerAttributesController = this.GetComponent<PlayerAttributesController>();
    }

    void OnEnable()
    {
        this.controls.Player.Enable();

        move = this.controls.Player.Move;
        move.performed += ctx => { OnMove(ctx.ReadValue<Vector2>()); };
        move.canceled += ctx => { OnCancelMove(ctx.ReadValue<Vector2>()); };


        shoot = this.controls.Player.Shoot;
        shoot.performed += ctx => { OnShoot(ctx.ReadValue<Vector2>()); };
        shoot.canceled += ctx => { OnCancelShoot(); };

        useActivatedItem = this.controls.Player.UseActivatedItem;
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

        mousePress = this.controls.Player.MousePress;
        mousePress.performed += ctx => { OnMouseLeftPress(); };
        mousePress.canceled += ctx => { OnCancelMouseLeftPress(); };

        mousePosition = this.controls.Player.MousePostion;
        mousePosition.performed += ctx => { OnMousePostion(ctx.ReadValue<Vector2>()); };

        useHealthPotion=this.controls.Player.UseHealthPotion;
        useHealthPotion.performed += ctx => { OnHealthPotionUse(); };
    }

    private void OnHealthPotionUse()
    {
        playerActionsController.useHealthPotion();
    }

    void OnDisable()
    {
        this.controls.Player.Disable();
    }

    //On actions
    void OnMove(Vector2 movement)
    {
        playerMovementController.setMoveValue(movement);
    }
    void OnCancelMove(Vector2 movement)
    {
        playerMovementController.setMoveValue(movement);
    }

    void OnShoot(Vector2 direction)
    {
        //Debug.Log("direction: "+ direction);
        this.playerActionsController.setShootingDiretion(direction);
        this.playerActionsController.setIsShooting(true);
    }
    void OnCancelShoot()
    {
        // Debug.Log("cancel shoot: ");
        this.playerActionsController.setIsShooting(false);

    }

    void OnUseActivatedItem()
    {
        Debug.Log("UseActivatedItem");
    }
    void OnCancelUseActivatedItem()
    {
        Debug.Log("CancelActivatedItem");
    }
    void OnUseSingleItem()
    {
        this.playerActionsController.useSingleUseItem();
    }
    void OnCancelUseSingleItem()
    {
        Debug.Log("CancelSingleItem");
    }
    void OnPut()
    {
        //Debug.Log("On put");
        playerActionsController.putObject();
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



    void OnMousePostion(Vector2 pos)
    {
        this.playerActionsController.setWorldMousePostion(Camera.main.ScreenToWorldPoint(pos));
    }



    void OnMouseLeftPress()
    {
        this.playerActionsController.setLeftMousePress(true);
        this.playerActionsController.setShootingDirectionFromMouse();
        this.playerActionsController.setIsShooting(true);

    }



    void OnCancelMouseLeftPress()
    {
        this.playerActionsController.setLeftMousePress(false);
        this.playerActionsController.setIsShooting(false);
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