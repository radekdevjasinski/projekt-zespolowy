using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    public float minFlickerDuration;
    public float maxFlickerDuration;
    public float minIntensityRange;
    public float maxIntensityRange;
    private Light2D light2D;

    void Start()
    {
        light2D = GetComponent<Light2D>();
        StartCoroutine(FlickerLight());
    }

    IEnumerator FlickerLight()
    {
        while (true)
        {
            float duration = Random.Range(minFlickerDuration, maxFlickerDuration);
            float intensity = Random.Range(minIntensityRange, maxIntensityRange);
            light2D.intensity = intensity;
            yield return new WaitForSeconds(duration);
        }
    }
}
