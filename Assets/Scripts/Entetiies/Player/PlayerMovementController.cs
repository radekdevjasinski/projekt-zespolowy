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

    private Rigidbody2D rb;
    private GameObject bombs;
    public GameObject bombPrefab;
    private Vector2 moveValue;
    private Vector2 shootValue;
    private Animator animator;
    public float speed;
    public float shootSpeed = 0.5f;
    private float timer = 0f;
    private float groundSpeedAffect= 1f;
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        playerAttributesController = this.gameObject.GetComponent<PlayerAttributesController>();
    }



    void FixedUpdate()
    {
;        rb.AddForce( moveValue*groundSpeedAffect * baseSpeed*(sppedAttributeMultiplayer*playerAttributesController.Speed));
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

}
