using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EntityController : MonoBehaviour
{
    protected float groundDragBase = 30f;

    abstract public void resetDrag();

    abstract public void setDrag(float drag);

    abstract public void setGroundSpeedAffect(float f);
}
