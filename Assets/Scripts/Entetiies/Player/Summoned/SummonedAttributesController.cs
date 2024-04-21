using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonedAttributesController : MonoBehaviour
{
    [SerializeField] private float health = 10f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float damage = 1f;

    public void Start()
    {
        currentHealth = health;
    }

    public void Update()
    {
        if (currentHealth <=0) 
        {
            Destroy(this.gameObject);
        }
    }

    public float GetHealth()
    {
        return health;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public float GetFireRate()
    {
        return fireRate;
    }
    public float GetDamage()    
    {
        return damage;
    }

    public void getDamage(float damage)
    {
        currentHealth -= damage;
    }
}
