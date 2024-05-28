using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAffect : Affect
{
    private SpriteRenderer parentSprite;
    private Material previousMaterial;
    [SerializeField] protected Material newMat;

    private void Awake()
    {
        Debug.Log("Heal affect");
        parentSprite=transform.parent.GetComponent<SpriteRenderer>();
    }
    protected override void affect()
    {
        previousMaterial = parentSprite.material;
        parentSprite.material = newMat;

    }

    protected override void deAffcet()
    {
        parentSprite.material = previousMaterial;
    }
}
