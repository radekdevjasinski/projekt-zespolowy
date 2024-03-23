using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionsController : MonoBehaviour
{

    [Header("Putting")]
    [SerializeField] private GameObject puttableItem;
    [Header("Shooting")]
    [SerializeField] private GameObject shootableItem;
    [SerializeField] private float shootSpeed;
    [SerializeField] private float shootCooldown;
    private Transform shootParent;
    [Header("Single item use")]

    [Header("Activated item use")]
    //states
    bool canShoot;
    bool isShooting;
    Vector2 direction;

    //components
    Collider2D collider;
    private Animator animator;

    private void Awake()
    {
        canShoot = true;
        this.collider=GetComponent<Collider2D>();
        shootParent = GameObject.Find("bombs").transform;
        animator = this.gameObject.GetComponent<Animator>();
    }

    private void shoot()
    {
    if(canShoot)
        {
            canShoot = false;
           
      
        Vector3 startPosition = new Vector3(
            transform.position.x + collider.bounds.size.x* 1.4f * direction.x,
            transform.position.y + collider.bounds.size.y*1.4f * direction.y,
            0f)
            ;
        GameObject shootable = Instantiate(
            shootableItem,
             startPosition,
           new Quaternion(0f,0f,0f,0f),
           shootParent
           );
        shootable.GetComponent<Rigidbody2D>().AddForce(direction * shootSpeed,ForceMode2D.Impulse);
           Invoke("resetShootingCooldDown", shootCooldown);
        }
    }


    private void resetShootingCooldDown()
    {
    this.canShoot = true;
   
        if(isShooting)
            this.shoot();
    }

    public void setIsShooting(bool state,Vector2 direction)
    {
        animator.SetBool("Shooting", state);
        animator.SetFloat("HorizontalShoot", direction.x);
        animator.SetFloat("VerticalShoot", direction.y);
        this.isShooting=state;
        this.direction = direction;
        this.shoot();
    }

}
