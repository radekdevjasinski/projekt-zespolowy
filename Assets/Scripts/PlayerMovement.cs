using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private float moveSpeed = 10f;
    private float friction = 6f;

    private PlayerControler playerControler = null;
    private Vector2 moveVector = Vector2.zero;

    public GameObject bullet;
    private Vector2 bulletVector = Vector2.zero;
    private bool isShooting = false;
    private float bulletSpeed = 8f;
    private float shootSpeed = 0.5f;
    private float shootTime = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerControler = new PlayerControler();
        //bullet = GameObject.FindGameObjectWithTag("Bullet");
    }

    private void FixedUpdate()
    {
        if (moveVector.magnitude < 0.1f)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, friction * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = moveVector.normalized * moveSpeed;
        }

        if (isShooting && Time.time - shootTime > shootSpeed)
        {
            Shoot();
            shootTime = Time.time;
        }

    }

    private void OnEnable()
    {
        playerControler.Enable();
        playerControler.Player.Move.performed += OnMovePerformed;
        playerControler.Player.Move.canceled += OnMoveCancelled;
        playerControler.Player.Fire.started += OnFireStarted;
        playerControler.Player.Fire.canceled += OnFireCanceled;
    }

    private void OnDisable()
    {
        playerControler.Disable();
        playerControler.Player.Move.performed -= OnMovePerformed;
        playerControler.Player.Move.canceled -= OnMoveCancelled;
        playerControler.Player.Fire.started -= OnFireStarted;
        playerControler.Player.Fire.canceled -= OnFireCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }
    private void OnMoveCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }
    private void OnFireStarted(InputAction.CallbackContext value)
    {
        bulletVector = value.ReadValue<Vector2>();
        isShooting = true;
    }

    private void OnFireCanceled(InputAction.CallbackContext value)
    {
        bulletVector = Vector2.zero;
        isShooting = false;
    }
    private void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity, null);
        newBullet.transform.position = new Vector3(transform.position.x + bulletVector.x * 0.5f, transform.position.y + bulletVector.y * 0.6f, transform.position.z);
        newBullet.GetComponent<Rigidbody2D>().velocity = bulletVector * bulletSpeed;
        //Destroy(newBullet, 2f);
    }


}
