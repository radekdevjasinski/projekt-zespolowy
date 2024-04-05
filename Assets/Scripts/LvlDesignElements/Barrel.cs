using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private bool hasStarted = false;
    public GameObject explosionPrefab;
    private DestroyableObject destroyableObject;

    private void Awake()
    {
        destroyableObject = GetComponent<DestroyableObject>();
    }

    private void FixedUpdate()
    {
        if( destroyableObject.health <= 0 && hasStarted == false)
        {
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.red;
            StartCoroutine(DestroyThisObject());
        }
    }

    private IEnumerator DestroyThisObject()
    {
        hasStarted = true;
        yield return new WaitForSeconds(1f);
        GameObject newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
