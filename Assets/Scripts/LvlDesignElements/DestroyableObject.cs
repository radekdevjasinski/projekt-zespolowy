using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public int health;

    public void GetDamage(float damage)
    {
        int damageInt = Mathf.RoundToInt(damage);
        health -= damageInt;
    }
}