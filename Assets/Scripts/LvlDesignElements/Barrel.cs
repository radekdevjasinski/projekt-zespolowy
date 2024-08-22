using System.Collections;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private bool hasStarted = false;
    [SerializeField] private GameObject blastPrefab;
    [SerializeField] private Material explosionMaterial;
    [SerializeField] private float explosionSpeed = 0.5f;
    [SerializeField] private float blastRadius = 0.5f;
    [SerializeField] private float damageModifier = 2.0f;  
    [SerializeField] private LayerMask damageLayerMask;
    private DestroyableObject destroyableObject;
    private NavigationBake navigationBake;
<<<<<<< HEAD
=======

>>>>>>> parent of 9b7bd51 (Revert "Merge remote-tracking branch 'origin/Destroyable-Objects'")

    private void Awake()
    {
        destroyableObject = GetComponent<DestroyableObject>();
        navigationBake = FindObjectOfType<NavigationBake>();
    }

    private void FixedUpdate()
    {
        if (destroyableObject.health <= 0 && hasStarted == false)
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

        GameObject blast = Instantiate(blastPrefab);
        blast.transform.position = this.transform.position;

        Renderer blastRenderer = blast.GetComponent<Renderer>();
        blastRenderer.material = explosionMaterial;

        var explosionControl = blast.AddComponent<ExplosionControl>();
        explosionControl.explosionMaterial = explosionMaterial;
        explosionControl.explosionSpeed = explosionSpeed;

        explosionControl.StartExplosion(0f, 0.5f);

        DealDamage();

        Destroy(this.gameObject);

<<<<<<< HEAD
=======

>>>>>>> parent of 9b7bd51 (Revert "Merge remote-tracking branch 'origin/Destroyable-Objects'")
        if (navigationBake != null)
        {
            Debug.LogError("bake");
            navigationBake.BakeNavMesh();
        }
        else
        {
            Debug.LogError("NavigationBake instance not found.");
        }
<<<<<<< HEAD
    }

    private void DealDamage()
    {
        Collider2D[] affectedObjects = Physics2D.OverlapCircleAll(transform.position, blastRadius, damageLayerMask);
        foreach (Collider2D obj in affectedObjects)
        {
            IDealDamage damageable = obj.GetComponent<IDealDamage>();
            if (damageable != null)
            {
                Vector2 direction = transform.position - obj.transform.position;
                damageable.dealDamageUniversal(damageModifier, direction);

            }
        }
=======
>>>>>>> parent of 9b7bd51 (Revert "Merge remote-tracking branch 'origin/Destroyable-Objects'")
    }
}
