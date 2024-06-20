using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDealDamage
{
    void dealDamageUniversal(float amount);
    void dealDamageUniversal(float damageModifer, Vector2 vector2);
}
