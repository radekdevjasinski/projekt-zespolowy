using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    //controlers
    PlayerAttributesController playerAttributesController;

    //Parameters
    [SerializeField] float baseSpeed;
    [SerializeField] private float sppedAttributeMultiplayer = 1;

    private float speed;
    private Rigidbody2D rb;
    private Vector2 moveValue;
    private Animator animator;
    private float groundSpeedAffect= 1f;
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        playerAttributesController = this.gameObject.GetComponent<PlayerAttributesController>();
    }



    void Update()
    {
        speed = groundSpeedAffect * baseSpeed*(sppedAttributeMultiplayer*playerAttributesController.Speed);
    }
    void FixedUpdate()
    {
;        rb.AddForce( moveValue * speed );
    }
  

    public void setMoveValue(Vector2 moveValue)
    {
        this.moveValue = moveValue;
        animator.SetFloat("HorizontalMove", moveValue.x);
        animator.SetFloat("VerticalMove", moveValue.y);
        animator.SetFloat("Speed", moveValue.sqrMagnitude);
    }

    public void setGroundSpeedAffect(float val)
    {
        this.groundSpeedAffect = val;
    }
    public float getSpeed()
    {
        return speed;
    }
    public Vector2 getMoveValue()
    {
        return moveValue;
    }

    public Vector2 getCurrentDirection()
    {
        return moveValue.normalized;
    }
}
