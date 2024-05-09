using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : PicableItem
{

    [SerializeField] private float value;
    [SerializeField] private PlayerAttributesController.attributes attribute;



    protected override void onItemPick(GameObject obj)
    {
        obj.GetComponent<PlayerAttributesController>().increaseWithlimit(attribute, value);
    }


}
