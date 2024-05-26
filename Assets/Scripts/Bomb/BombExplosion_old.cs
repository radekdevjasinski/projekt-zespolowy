using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion_old : MonoBehaviour
{
    public GameObject blastPrefab;
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (!collision.gameObject.CompareTag("Player"))
        {
            GameObject blast = Instantiate(blastPrefab);
            //Obrazania zadawane beczkom
            if (collision.gameObject.GetComponent<Barrel>() != null)
            {
                collision.gameObject.GetComponent<DestroyableObject>().GetDamage(2);
            }
            //
            blast.transform.position = this.transform.position;
            blast.GetComponent<ParticleSystem>().Play();
            Destroy(blast, 3f);
            Destroy(this.gameObject);
        }
    }

}
