using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerEntityController : EntityController
{
    public override void resetDrag()
    {
        Debug.Log("reset drag");
        this.GetComponent<Rigidbody2D>().drag = this.groundDragBase;
    }

    public override void setDrag(float drag)
    {
        Debug.Log("set drag");
        this.GetComponent<Rigidbody2D>().drag=drag;
    }

    public override void setGroundSpeedAffect(float f)
    {
      this.GetComponent<PlayerMovementController>().setGroundSpeedAffect(f);
    }

    protected override void reviceDamage(int damage)
    {
        this.GetComponent<PlayerAttributesController>().increaseHealth(-damage);
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
        Destroy(this.gameObject);
        GameObject gameOverScreen = GameObject.Find("GameOver");
        gameOverScreen.GetComponent<GameOverScreen>().Setup();

    }
}
