using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostAffect : Affect
{
    [SerializeField] private PlayerAttributesController.attributes attrbuteAdjust;
    [SerializeField] private float attributeChange;


    protected override void affect()
    {
      this.GetComponentInParent<PlayerAttributesController>().increaseWithlimit(attrbuteAdjust, attributeChange);
    }

    protected override void deAffcet()
    {
        this.GetComponentInParent<PlayerAttributesController>().increaseWithlimit(attrbuteAdjust, -attributeChange);
    }
}
