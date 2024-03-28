using System.Collections;
using System.Collections.Generic;
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
}
