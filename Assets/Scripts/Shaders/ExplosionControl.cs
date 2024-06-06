using System.Collections;
using UnityEngine;

public class ExplosionControl : MonoBehaviour
{
    [SerializeField] private string materialName = "ExplosionMaterial";

    [SerializeField] public Material explosionMaterial;
    [SerializeField] public float explosionSpeed = 0.75f;

    private static int _timeToPlay = Shader.PropertyToID("_TimeToPlay");



    public void StartExplosion(float startPos, float endPos)
    {
        StartCoroutine(ExplosionAnimation(startPos, endPos));
    }

    private IEnumerator ExplosionAnimation(float startPos, float endPos)
    {
        explosionMaterial.SetFloat(_timeToPlay, startPos);

        float lerpedAmount = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < explosionSpeed && lerpedAmount != 1)
        {
            elapsedTime += Time.deltaTime;

            lerpedAmount = Mathf.Lerp(startPos, endPos, elapsedTime / explosionSpeed);
            explosionMaterial.SetFloat(_timeToPlay, lerpedAmount);

            yield return null;
        }

        Destroy(gameObject);
    }
}
