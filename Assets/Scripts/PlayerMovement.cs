using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //private Controls controls;
    //private Rigidbody2D rb;
    //private GameObject bombs;
    //public GameObject bombPrefab;
    //private Vector2 moveValue;
    //private Vector2 shootValue;
    //private Animator animator;
    //public float speed;
    //public float shootSpeed = 0.5f;
    //private float timer = 0f;
    //void Awake()
    //{
    //    controls = new Controls();
    //    rb = GetComponent<Rigidbody2D>();
    //    controls.Player.Enable();
    //    animator = this.gameObject.GetComponent<Animator>();
    //    bombs = GameObject.Find("bombs");
    //}
    //void FixedUpdate()
    //{
    //    moveValue = controls.Player.Movement.ReadValue<Vector2>();
    //    animator.SetFloat("Horizontal", moveValue.x);
    //    animator.SetFloat("Vertical", moveValue.y);
    //    animator.SetFloat("Speed", moveValue.sqrMagnitude);
    //    rb.velocity = moveValue * speed;

    //    shootValue = controls.Player.Shooting.ReadValue<Vector2>();
    //    if (shootValue.magnitude > 0.0001f)
    //    {
    //        animator.SetBool("Shooting", true);
    //        animator.SetFloat("Horizontal", shootValue.x);
    //        animator.SetFloat("Vertical", shootValue.y);

    //        Shoot();
    //    }
    //    else
    //    {
    //        animator.SetBool("Shooting", false);
    //    }
    //    if (timer >= 0 )
    //    {
    //        timer -= Time.deltaTime;
    //    }

    //}
    //void Shoot()
    //{
    //    if (timer <= 0)
    //    {
    //        GameObject bomb = Instantiate(bombPrefab, bombs.transform);
    //        bomb.transform.position = new Vector3(transform.position.x + shootValue.x * 0.2f, transform.position.y + shootValue.y * 0.4f, transform.position.z);
    //        bomb.GetComponent<Rigidbody2D>().velocity = shootValue * speed * 2;

    //        timer = shootSpeed;
    //    }

    //}
}
