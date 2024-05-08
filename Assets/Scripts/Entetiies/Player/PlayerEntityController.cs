using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerEntityController : EntityController<int>
{

    // Code Functionality from previous Entity controller
    // that included also gorund affected behaviour
    // I left if there was a need for changing behaviour based on ground later


    //public override void resetDrag()
    //{
    //    Debug.Log("reset drag");
    //    this.GetComponent<Rigidbody2D>().drag = this.groundDragBase;
    //}

    //public override void setDrag(float drag)
    //{
    //    Debug.Log("set drag");
    //    this.GetComponent<Rigidbody2D>().drag=drag;
    //}

    //public override void setGroundSpeedAffect(float f)
    //{
    //  this.GetComponent<PlayerMovementController>().setGroundSpeedAffect(f);
    //}
    [SerializeField] private float invincLength = 1;
    private Rigidbody2D rb;
    public bool isKnocked;
    [SerializeField] private float knockbackForce = 20f;
    [SerializeField] private float knockbackDuration = 1f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (invincCount > 0)
        {
            invincCount -= Time.deltaTime;
        }
    }
    protected override int getMaxHealth()
    {
        throw new NotImplementedException(); // no heatlh limit for player
    }

    public override void reviceDamage(int damage, Vector2 damageDirection)
    {
        invincCount = invincLength;

        // Odwracamy kierunek odrzutu
        Vector2 knockbackDirection = -damageDirection.normalized;

        StartCoroutine(HitKnockback(knockbackDirection));

        GetComponent<PlayerAttributesController>().increaseHealth(-damage);
        GameObject HpBar = GameObject.Find("HpBar");
        HpBar.GetComponent<HealtHeartBar>().DrawHearts();
    }
    public override void reviceDamage(int damage)
    {

    }
    protected virtual IEnumerator HitKnockback(Vector2 knockbackDirection)
    {
        isKnocked = true;


        rb.velocity = knockbackDirection * knockbackForce;

        yield return new WaitForSeconds(knockbackDuration);

        isKnocked = false;
    }


    public void heal()
    {
        GetComponent<PlayerAttributesController>().resetHealth();
        GameObject HpBar = GameObject.Find("HpBar");
        HpBar.GetComponent<HealtHeartBar>().DrawHearts();
    }

    protected override int getHealth()
    {
        return
            this.GetComponent<PlayerAttributesController>().Health;
    }

    protected override void onDie()
    {
        //Destroy(this.gameObject);
        GetComponent<PlayerControler>().lockInput();
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GameObject gameOverScreen = GameObject.Find("GameOver");
        gameOverScreen.GetComponent<GameOverScreen>().Setup();

    }
}
