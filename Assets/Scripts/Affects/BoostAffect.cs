using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostAffect : Affect
{
    [SerializeField] private PlayerAttributesController.attributes attrbuteAdjust;
    [SerializeField] private int attributeChange;


    protected override void affect()
    {
      this.GetComponentInParent<PlayerAttributesController>().increase(attrbuteAdjust, attributeChange);
    }

    protected override void deAffcet()
    {
        this.GetComponentInParent<PlayerAttributesController>().increase(attrbuteAdjust, -attributeChange);
    }
}
