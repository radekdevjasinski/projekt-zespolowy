using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public int health;

    public void GetDamage(int damage)
    {
        health -= damage;
    }
}