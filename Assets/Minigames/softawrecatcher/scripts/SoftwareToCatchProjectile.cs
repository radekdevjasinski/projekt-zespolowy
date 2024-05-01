using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftwareToCatchProjectile : Projectile
{

    [SerializeField] private GameObject PrefabOnCaought;
    [SerializeField] private GameObject PrefabOnMissed;


    protected override void OnDestroyed()
    {
        Debug.Log("Destroyrd");
    }

    protected override void onHit(GameObject obj)
    {
        if (obj.CompareTag("Collider"))
        {
            gameObject.AddComponent<InstainiteOnDestroy>().setObjectToInstance(PrefabOnMissed);
            OnDeath();
        }
        else
        {   gameObject.AddComponent<InstainiteOnDestroy>().setObjectToInstance(PrefabOnCaought);
        }
    }

    private void OnDeath()
    {
        DeathSeuance[] death = GetComponents<DeathSeuance>();
        foreach (DeathSeuance var in death)
        {
            var.onDeath();
        }
    }
}
