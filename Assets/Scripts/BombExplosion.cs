using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    [SerializeField] private GameObject blastPrefab;

    [SerializeField] private float explosionTimer = 3;
    void Start()
    {
        Invoke("kill", explosionTimer);
        
    }

    private void kill()
    {
        GameObject blast = Instantiate(blastPrefab);
        blast.transform.position = this.transform.position;
        blast.GetComponent<ParticleSystem>().Play();
        Destroy(blast, 3f);
        Destroy(this.gameObject);
    }

}
