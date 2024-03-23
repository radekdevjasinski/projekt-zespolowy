using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{


    //Parameters
    [SerializeField] float baseSpeed;


    private Rigidbody2D rb;
    private GameObject bombs;
    public GameObject bombPrefab;
    private Vector2 moveValue;
    private Vector2 shootValue;
    private Animator animator;
    public float speed;
    public float shootSpeed = 0.5f;
    private float timer = 0f;
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        //bombs = GameObject.Find("bombs");
    }



    void FixedUpdate()
    {
        //moveValue = controls.Player.Movement.ReadValue<Vector2>();

;        rb.AddForce( moveValue * baseSpeed);

        //shootValue = controls.Player.Shooting.ReadValue<Vector2>();
        //if (shootValue.magnitude > 0.0001f)
        //{
        //    animator.SetBool("Shooting", true);
        //    animator.SetFloat("Horizontal", shootValue.x);
        //    animator.SetFloat("Vertical", shootValue.y);

        //    Shoot();
        //}
        //else
        //{
        //    animator.SetBool("Shooting", false);
        //}
        //if (timer >= 0)
        //{
        //    timer -= Time.deltaTime;
        //}

    }
  

    public void setMoveValue(Vector2 moveValue)
    {
        Debug.Log("set move vmlaue: "+ moveValue);
        this.moveValue = moveValue;
        animator.SetFloat("HorizontalMove", moveValue.x);
        animator.SetFloat("VerticalMove", moveValue.y);
        animator.SetFloat("Speed", moveValue.sqrMagnitude);
    }

}
