using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : PicableItem
{

    [SerializeField] private int value;
    [SerializeField] private PlayerAttributesController.attributes attribute;



    protected override void onItemPick(GameObject obj)
    {
        obj.GetComponent<PlayerAttributesController>().increase(attribute, value);
    }


}
