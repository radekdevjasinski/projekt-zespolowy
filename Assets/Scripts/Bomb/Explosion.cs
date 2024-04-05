using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damageExplosion = 600;
    public float delay = 0.3f;
    void Start()
    {
        Destroy(gameObject, delay);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<DestroyableObject>() != null)
        {
            collision.gameObject.GetComponent<DestroyableObject>().GetDamage(damageExplosion);
        }
    }
}
