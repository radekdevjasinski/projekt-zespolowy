using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EntityController : MonoBehaviour
{

    [SerializeField] private GameObject soundOnDamage;

    protected float groundDragBase = 30f;

    abstract public void resetDrag();

    abstract public void setDrag(float drag);

    abstract public void setGroundSpeedAffect(float f);

    public void dealDamage(int baseDamage)
    {
        if(this.soundOnDamage!=null)
        SoundManager.instance.playSound(this.transform,this.soundOnDamage,this.transform.position);
        this.reviceDamage(baseDamage);
        if (this.getHealth() == 0)
        {
            this.onDie();


            this.GetComponents<DeathSeuance>();
        }
    }

    protected void runDeathSequanceElemnts()
    {
        foreach (DeathSeuance var in this.GetComponents<DeathSeuance>())
        {
            var.onDeath();

        }
    }


    protected abstract void onDie();

    protected abstract int getHealth();

    protected abstract void reviceDamage(int damage);
}
