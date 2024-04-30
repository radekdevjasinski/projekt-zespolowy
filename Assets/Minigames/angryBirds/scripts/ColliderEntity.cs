using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEntity : EntityController<int>
{
    [SerializeField] private int Maxhealth = 3;
    [SerializeField] private int health=3;

    protected override void onDie()
    {
      Destroy(gameObject);
    }

    public override int getHealth()
    {
        return health;
    }

    public override int getMaxHealth()
    {
        return Maxhealth;
    }

    public override void reviceDamage(int damage)
    {
        health-=damage;
    }
}
