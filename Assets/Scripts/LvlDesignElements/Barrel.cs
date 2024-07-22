using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private bool hasStarted = false;
    public GameObject explosionPrefab;
    private DestroyableObject destroyableObject;
    private NavigationBake navigationBake;


    private void Awake()
    {
        destroyableObject = GetComponent<DestroyableObject>();
        navigationBake = FindObjectOfType<NavigationBake>();
    }

    private void FixedUpdate()
    {
        if( destroyableObject.health <= 0 && hasStarted == false)
        {
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.red;
            StartCoroutine(DestroyThisObject());
            //navigationBake.BakeNavMesh();
        }
    }

    private IEnumerator DestroyThisObject()
    {
        hasStarted = true;
        yield return new WaitForSeconds(1f);
        GameObject newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);


        if (navigationBake != null)
        {
            Debug.LogError("bake");
            navigationBake.BakeNavMesh();
        }
        else
        {
            Debug.LogError("NavigationBake instance not found.");
        }
    }
}
