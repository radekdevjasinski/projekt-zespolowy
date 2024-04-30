using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ColliderEntity))]
public class angryBirdsColldier : MonoBehaviour
{
    private Rigidbody2D rb;
    private EntityController<int> entity;

    [SerializeField] private float forceResistance = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        entity= GetComponent<EntityController<int>>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D: "+col.relativeVelocity.magnitude);
        float force = col.relativeVelocity.magnitude *
                      col.otherCollider.attachedRigidbody.mass;
        if (force  >= forceResistance)
        {
            entity.dealDamage((int)(col.relativeVelocity.magnitude / forceResistance));
        }
    }
}
