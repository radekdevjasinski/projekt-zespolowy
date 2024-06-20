using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BombExplosion : PuttableObject
{
    [SerializeField] private GameObject blastPrefab;
    [SerializeField] private GameObject shockWavePrefab;

    [SerializeField] private Material explosionMaterial;

    [SerializeField] private float explosionTimer = 3f;
    [SerializeField] private float explosionSpeed = 0.75f;
    [SerializeField] private float blastRadius = 1;
    [SerializeField] private LayerMask destructableMask;
    [Header("Damage")]
    [SerializeField] private float damagRadius;
    [SerializeField] private float damageModifer;
    [SerializeField] private LayerMask damgaeLayerMask;
    [SerializeField] private float knockbackPower;
    private Coroutine _coroutine;

    [SerializeField]


    protected override void onPut()
    {
        Debug.Log("onPut() called");
        Invoke("CallExplosion", explosionTimer);
    }

    private void destroyObjects()
    {
        Collider2D[] objectToDestroy = Physics2D.OverlapCircleAll(transform.position, blastRadius, destructableMask);
        foreach (Collider2D obj in objectToDestroy)
        {
            obj.GetComponent<Destructable>().destruct();
        }
    }

    public void CallExplosion()
    {
        _coroutine = StartCoroutine(ExplosionAction(0f, 1f));
        Destroy(this.gameObject);
    }


    void dealDamge()
    {
        Collider2D[] dealDamages = Physics2D.OverlapCircleAll(transform.position, damagRadius, damgaeLayerMask)
            .Where((collider2D1 => collider2D1.GetComponent<IDealDamage>() != null)).ToArray();
        foreach (Collider2D item in dealDamages)
        {
           item.GetComponent<IDealDamage>().dealDamageUniversal(damageModifer, transform.position - item.transform.position);
      
        }


    }

    private IEnumerator ExplosionAction(float startPos, float endPos)
    {
        GameObject blast = Instantiate(blastPrefab);
        blast.transform.position = this.transform.position;

        Renderer blastRenderer = blast.GetComponent<Renderer>();
        blastRenderer.material = explosionMaterial;

        GameObject shockWave = Instantiate(shockWavePrefab, transform.position, Quaternion.identity);
        var shockWaveControl = shockWave.GetComponent<ShockWaveControl>();
        shockWaveControl.CallShockWave();

        var explosionControl = blast.AddComponent<ExplosionControl>();
        explosionControl.explosionMaterial = explosionMaterial;
        explosionControl.explosionSpeed = explosionSpeed;

        explosionControl.StartExplosion(startPos, endPos);


        //destroyObjects(); currently no object to destroy so turned off
        dealDamge();
        yield return null;
    }



}
