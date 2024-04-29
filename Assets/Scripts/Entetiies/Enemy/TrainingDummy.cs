using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummy : EntityController<float>
{
    [SerializeField]
    private float maxHelath = 100;
    [SerializeField]
    private float health=100;

    public override float getHealth()
    {
       return health;
    }

    public override float getMaxHealth()
    {
       return maxHelath;
    }

    public override void reviceDamage(float damage)
    {
        Debug.Log("dummy recived damgage: " + damage);
        health -= damage;
    }

    protected override void onDie()
    {
     Destroy(gameObject);
    }
}
