using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Potion : SingleUseItem
{
    [SerializeField] private GameObject affect;
    protected override void onUse(GameObject obj)
    {
        Instantiate(affect, this.GetComponentInParent<Transform>(), true);
    }


}
