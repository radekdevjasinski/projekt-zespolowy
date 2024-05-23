using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerEntityController : EntityController<int>
{

    private PlayerAttributesController playerAttributesController;

    [SerializeField] private float invincLength = 1;
    private Rigidbody2D rb;
    public bool isKnocked;
    [SerializeField] private float knockbackForce = 20f;
    [SerializeField] private float knockbackDuration = 1f;

    protected void Awake()
    {
        playerAttributesController = GetComponent<PlayerAttributesController>();
        rb = GetComponent<Rigidbody2D>();
    }


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
    

    private void Update()
    {
        if (invincCount > 0)
        {
            invincCount -= Time.deltaTime;
        }
    }
    public override int getMaxHealth()
    {
        return playerAttributesController.getMaxHealth();


    }

    public override void reviceDamage(int damage, Vector2 damageDirection)
    {
        if (playerAttributesController.Armor > 0)
        {
            playerAttributesController.increaseArmor(-damage);
            GameObject ArmorBar = GameObject.Find("ArmorBar");
            ArmorBar.GetComponent<ArmorBar>().DrawArmor();
        }
        else
        {
            playerAttributesController.increaseHealth(-damage);
            GameObject HpBar = GameObject.Find("HpBar");
            HpBar.GetComponent<HealtHeartBar>().DrawHearts();
        }
        
        invincCount = invincLength;
        // Odwracamy kierunek odrzutu
        Vector2 knockbackDirection = -damageDirection.normalized;
        StartCoroutine(HitKnockback(knockbackDirection));

    }
    public override void reviceDamage(int damage)
    {
        if (playerAttributesController.Armor > 0)
        {
            playerAttributesController.increaseArmor(-damage);
            GameObject ArmorBar = GameObject.Find("ArmorBar");
            ArmorBar.GetComponent<ArmorBar>().DrawArmor();
        }
        else
        {
            playerAttributesController.increaseHealth(-damage);
            GameObject HpBar = GameObject.Find("HpBar");
            HpBar.GetComponent<HealtHeartBar>().DrawHearts();
        }

        invincCount = invincLength;

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
        playerAttributesController.resetHealth();
        GameObject HpBar = GameObject.Find("HpBar");
        HpBar.GetComponent<HealtHeartBar>().DrawHearts();
    }
    public void heal(int amount)
    {
        GetComponent<PlayerAttributesController>().increaseHealth(amount);
        GameObject HpBar = GameObject.Find("HpBar");
        HpBar.GetComponent<HealtHeartBar>().DrawHearts();
    }

    public void addArmor(int amount)
    {
        playerAttributesController.increaseArmor(amount);
        GameObject ArmorBar = GameObject.Find("ArmorBar");
        ArmorBar.GetComponent<ArmorBar>().DrawArmor();

    }

    public override int getHealth()
    {
        return
           playerAttributesController.Health;
    }

    protected override void onDie()
    {
        //Destroy(this.gameObject);
        GetComponent<PlayerControler>().lockInput();
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GameObject gameOverScreen = GameObject.Find("GameOver");
        gameOverScreen.GetComponent<GameOverScreen>().Setup();
        Time.timeScale = 0;
        PauseMenuController.GameIsPaused = true;
    }
}
