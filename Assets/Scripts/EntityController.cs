using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

abstract public class EntityController<TH> : MonoBehaviour
{


    

    //protected float groundDragBase = 30f;

    //abstract public void resetDrag();

    //abstract public void setDrag(float drag);

    //abstract public void setGroundSpeedAffect(float f);

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

    public void dealDamage(TH baseDamage)
    {
        runOnDamageBehaviour();
        this.reviceDamage(baseDamage);
        if (converToFloat(this.getHealth()) <= 0.0f)
        {
            killImidielty();
        }
    }

    public void killImidielty()
    {
        this.onDie();
        runDeathSequanceElemnts();
    }

    protected void runDeathSequanceElemnts()
    {
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

    protected abstract TH getHealth();
    protected abstract TH getMaxHealth();

    public abstract void reviceDamage(TH damage);
}
