using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : GroundAffect
{
    [SerializeField]
    private float iceDrag=2f;
    //protected override void onEnterGround(EntityController entity)
    //{
    //    //Turned off currently for standarizing entity 
    //    // scripts, If there is ever need to comback to that
    //    // functionality tere might a need to rethink it a little bir

    //    //entity.setDrag(iceDrag);
    //    //entity.setGroundSpeedAffect(0.2f);
    //}

    //protected override void onExitGround(EntityController entity)
    //{
    //    //Turned off currently for standarizing entity 
    //    // scripts, If there is ever need to comback to that
    //    // functionality tere might a need to rethink it a little bir


    //    //entity.resetDrag();
    //    //entity.setGroundSpeedAffect(1f);
    //}
}
