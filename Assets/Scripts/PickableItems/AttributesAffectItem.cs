using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesAffectItem : PicableItem
{
    [Serializable]
    public struct attributeToEdit
    {
        [SerializeField]
        public PlayerAttributesController.attributes attribute;
        [SerializeField]
        [Range(-100, 100)]
        [Tooltip("in proecent")]
        public int factor;
    }


    [SerializeField]
    private attributeToEdit[] Attributes;

    protected override void onItemPick(GameObject obj)
    {
        PlayerAttributesController controller = obj.GetComponent<PlayerAttributesController>();

        foreach (attributeToEdit attributeToEdit in Attributes)
        {
            controller.increase(attributeToEdit.attribute, (int)(attributeToEdit.factor));
        }

    }
}
