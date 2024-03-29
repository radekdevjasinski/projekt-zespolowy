using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EntityController : MonoBehaviour
{
    protected float groundDragBase = 30f;

    abstract public void resetDrag();

    abstract public void setDrag(float drag);

    abstract public void setGroundSpeedAffect(float f);

    public void dealDamage(int baseDamage)
    {
        this.reviceDamage(baseDamage);
        if (this.getHealth() == 0)
        {
            this.onDie();
        }
    }

    protected abstract void onDie();

    protected abstract int getHealth();

    protected abstract void reviceDamage(int damage);
}
