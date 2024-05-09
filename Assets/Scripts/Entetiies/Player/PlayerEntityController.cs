using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerEntityController : EntityController<int>
{

    private PlayerAttributesController playerAttributesController;

    protected void Awake()
    {
        playerAttributesController = GetComponent<PlayerAttributesController>();
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
    public override int getMaxHealth()
    {
        return playerAttributesController.getMaxHealth();


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

    }
}
