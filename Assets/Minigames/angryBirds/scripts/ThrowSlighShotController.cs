using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SlingShototerActionContrrller))]
public class ThrowSlighShotController : MonoBehaviour
{
    private Controls controls;
    private InputAction mouseClick;
    private InputAction mousePosition;
    private SlingShototerActionContrrller shototerActionContrrller;
    private bool isRelaoding = true;
    

    private void Awake()
    {

        this.controls = new Controls();
        shototerActionContrrller = GetComponent<SlingShototerActionContrrller>();
    }
    void OnEnable()
    {
        this.controls.Player.Enable();

        mouseClick = this.controls.Player.MousePress;
        mouseClick.performed += ctx => { onClick(); };
        mouseClick.canceled += ctx => { onCancelClick(); };


        mousePosition = this.controls.Player.MousePostion;
        mousePosition.performed += ctx => { OnMousePostion(ctx.ReadValue<Vector2>()); };

    }
    void OnMousePostion(Vector2 pos)
    {
        this.shototerActionContrrller.setWorldMousePostion(Camera.main.ScreenToWorldPoint(pos));
    }


    private void onClick()
    {
        Debug.Log("click");
        this.shototerActionContrrller.setIsHolding();;
    }

    private void onCancelClick()
    {
        Debug.Log("CancelClick");
        this.shototerActionContrrller.shoot();
    }

  
}
