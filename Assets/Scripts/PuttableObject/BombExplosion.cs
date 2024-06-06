using System.Collections;
using UnityEngine;

public class BombExplosion : PuttableObject
{
    [SerializeField] private GameObject blastPrefab;
    [SerializeField] private Material explosionMaterial;

    [SerializeField] private float explosionTimer = 3f;
    [SerializeField] private float explosionSpeed = 0.75f;
    [SerializeField] private float blastRadius = 1;
    [SerializeField] private LayerMask destructableMask;

    private Coroutine _coroutine;

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

    private IEnumerator ExplosionAction(float startPos, float endPos)
    {
        GameObject blast = Instantiate(blastPrefab);
        blast.transform.position = this.transform.position;

        Renderer blastRenderer = blast.GetComponent<Renderer>();
        blastRenderer.material = explosionMaterial;

        var explosionControl = blast.AddComponent<ExplosionControl>();
        explosionControl.explosionMaterial = explosionMaterial;
        explosionControl.explosionSpeed = explosionSpeed;

        explosionControl.StartExplosion(startPos, endPos);

        destroyObjects();

        yield return null;
    }
}
