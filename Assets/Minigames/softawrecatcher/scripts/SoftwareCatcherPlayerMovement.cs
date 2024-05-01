using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftwareCatcherPlayerMovement : MonoBehaviour
{

    //Parameters
    [SerializeField] float baseSpeed;

    private Rigidbody2D rb;
    private Vector2 moveValue;
    private Animator animator;

    [SerializeField] private Transform basketHodler;
    [SerializeField] private float basketOffset = 5;
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
    }



    void FixedUpdate()
    {
        rb.AddForce(moveValue *  baseSpeed);
    }


    public void setMoveValue(Vector2 moveValue)
    {
        moveValue.y = 0;
        Debug.Log("move value: " + moveValue);
        this.moveValue = moveValue;
        animator.SetFloat("HorizontalMove", moveValue.x);
        animator.SetFloat("VerticalMove", moveValue.y);
        animator.SetFloat("Speed", moveValue.sqrMagnitude);
        basketHodler.localPosition= new Vector3(moveValue.x*basketOffset, basketHodler.localPosition.y,0);
     
    }


}
