using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : GroundAffect
{
    [SerializeField]
    private float iceDrag=2f;
    protected override void onEnterGround(EntityController entity)
    {
        entity.setDrag(iceDrag);
        entity.setGroundSpeedAffect(0.2f);
    }

    protected override void onExitGround(EntityController entity)
    {
        entity.resetDrag();
        entity.setGroundSpeedAffect(1f);
    }
}
