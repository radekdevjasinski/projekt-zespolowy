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

    protected override int getMaxHealth()
    {
        throw new NotImplementedException(); // no heatlh limit for player
    }

    public override void reviceDamage(int damage)
    {
        GetComponent<PlayerAttributesController>().increaseHealth(-damage);
        GameObject HpBar = GameObject.Find("HpBar");
        HpBar.GetComponent<HealtHeartBar>().DrawHearts();    //health bar turned off for testing purposes
    }

    protected override int getHealth()
    {
        return
            this.GetComponent<PlayerAttributesController>().Health;
    }

    protected override void onDie()
    {
        Destroy(this.gameObject);
        GameObject gameOverScreen = GameObject.Find("GameOver");
        gameOverScreen.GetComponent<GameOverScreen>().Setup();

    }
}
