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
        Debug.Log("Awake");
        this.controls = new Controls();
    }

    private void Start()
    {
        Debug.Log("Start");
        shototerActionContrrller = GetComponent<SlingShototerActionContrrller>();

    }
    void OnEnable()
    {
        Debug.Log("OnEnable");
        this.controls.Player.Enable();

        mouseClick = this.controls.Player.MousePress;
        mouseClick.performed += ctx => { onClick(); };
        mouseClick.canceled += ctx => { onCancelClick(); };


        mousePosition = this.controls.Player.MousePostion;
        mousePosition.performed += ctx => { OnMousePostion(ctx.ReadValue<Vector2>()); };

    }

    void OnDisable()
    {
        this.controls.Player.Disable();
    }
    void OnMousePostion(Vector2 pos)
    {
        this.shototerActionContrrller.setWorldMousePostion(Camera.main.ScreenToWorldPoint(pos));
    }


    private void onClick()
    {
        Debug.Log("click");
        if (this.shototerActionContrrller == null)
        {
            this.shototerActionContrrller = GetComponent<SlingShototerActionContrrller>();
        }
        this.shototerActionContrrller.setIsHolding();;
    }

    private void onCancelClick()
    {
        Debug.Log("CancelClick");
        if (this.shototerActionContrrller == null)
        {
            this.shototerActionContrrller = GetComponent<SlingShototerActionContrrller>();
        }
        this.shototerActionContrrller.shoot();
    }

    private void onDestroy()
    {
        controls.Disable();
        controls.Dispose();
    }
  
}
