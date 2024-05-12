using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

abstract public class EntityController<TH> : MonoBehaviour, InvurnabilityControl
{
    protected float invincCount = 0;
    
    //protected float groundDragBase = 30f;

    //abstract public void resetDrag();

    //abstract public void setDrag(float drag);

    //abstract public void setGroundSpeedAffect(float f);
    [SerializeField]
    private bool invulnerable = false;



    private bool isAlive = true;
    private float converToFloat(TH val)
    {
        //if (typeof(float).IsAssignableFrom(typeof(TH)))
        //{
            return (float)Convert.ChangeType(val, typeof(float));
        //}
        //else
        //{
        //    throw new InvalidCastException("Cannot ocnverrt type");
        //}
    }



    public void setIsInvurnable(bool state)
    {
        this.invulnerable = state;
    }

    public void dealDamage(TH baseDamage, Vector2 damageDirection)
    {
        if (!this.invulnerable && this.invincCount <=0 )
        {
            runOnDamageBehaviour();
            this.reviceDamage(baseDamage, damageDirection);
            Debug.Log("Recived damage: "+ baseDamage+" to: "+ this.getHealth());
            if (converToFloat(this.getHealth()) <= 0.0f && isAlive==true)
            {
                killImidielty();
            }
        }
    }
    public void dealDamage(TH baseDamage)
    {
        if (!this.invulnerable && this.invincCount <= 0)
        {
            runOnDamageBehaviour();
            this.reviceDamage(baseDamage);
            Debug.Log("Recived damage: " + baseDamage + " to: " + this.getHealth());
            if (converToFloat(this.getHealth()) <= 0.0f && isAlive == true)
            {
                killImidielty();
            }
        }
    }



    public void killImidielty()
    {
        isAlive = false;
        this.onDie();
        runDeathSequanceElemnts();
    }

    protected void runDeathSequanceElemnts()
    {
        Debug.Log("----------runDeathSequanceElemnts");
        foreach (DeathSeuance var in this.GetComponents<DeathSeuance>())
        {
            var.onDeath();
        }
    }

    protected void runOnDamageBehaviour()
    {
        foreach (OnDamage var in this.GetComponents<OnDamage>())
        {
            var.onDamage();
        }
    }


    protected abstract void onDie();

    public abstract TH getHealth();
    public abstract TH getMaxHealth();

    public abstract void reviceDamage(TH damage, Vector2 damageDirection);
    public abstract void reviceDamage(TH damage);
    public bool getIsInvurnable()
    {
        return this.invulnerable;
    }

}
