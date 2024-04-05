using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    private void Start()
    {
        StartCoroutine(MakeExplosion());
    }
    private IEnumerator MakeExplosion()
    {
        yield return new WaitForSeconds(1f);
        GameObject newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
