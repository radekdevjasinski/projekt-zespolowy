using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    [SerializeField] private GameObject blastPrefab;

    [SerializeField] private float explosionTimer = 3;
    [SerializeField] private float blastRadius = 1;
    [SerializeField] private LayerMask destructableMask;
    void Start()
    {
        Invoke("kill", explosionTimer);
        
    }

    private void destroyObjects()
    {
        Collider2D[] objectToDestroy = Physics2D.OverlapCircleAll(transform.position, blastRadius, destructableMask);
        foreach (Collider2D obj in objectToDestroy)
        {
           // Debug.Log(obj.name);
            obj.GetComponent<Destructable>().destruct();
        }
    }

    private void kill()
    {
        GameObject blast = Instantiate(blastPrefab);
        blast.transform.position = this.transform.position;
        blast.GetComponent<ParticleSystem>().Play();
        destroyObjects();
        Destroy(blast, 3f);
        Destroy(this.gameObject);
    }

}
