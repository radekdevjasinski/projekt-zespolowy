using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] GunController gun;
    float speed = 5f;
    float fireRate = 0.5f;
    float nextFireTime;
    Vector2 mousePosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        rb.velocity = new Vector2(value.x,value.y) * speed;
        Debug.Log("Moving");
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if(Time.time >= nextFireTime)
        {
            Debug.Log("Shooting");
            gun.Fire();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
}
