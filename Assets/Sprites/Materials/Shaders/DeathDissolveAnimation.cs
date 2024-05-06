using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDissolveAnimation : MonoBehaviour
{
    [SerializeField] private Material material;
    //[ColorUsageAttribute(true,true)] --HDR color
    [SerializeField] private Color startColor;
    //[ColorUsageAttribute(true, true)]
    [SerializeField] private Color stopColor;
    private float dissolveSpeed;
    public float dissolveAmount;
    private bool isDissolving;


    private void Update()
    {
        if(isDissolving)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount + dissolveSpeed * Time.deltaTime);
            material.SetFloat("_DissolveAmount", dissolveAmount);
        }
        else
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount - dissolveSpeed * Time.deltaTime);
            material.SetFloat("_DissolveAmount", dissolveAmount);
        }
    }

    public void StartDissolving(float dissolveSpeed)
    {
        isDissolving = true;
        material.SetColor("_DissolveColor", startColor);
        this.dissolveSpeed = dissolveSpeed;
    }

    public void StopDissolving(float dissolveSpeed)
    {
        isDissolving = false;
        material.SetColor("_DissolveColor", stopColor);
        this.dissolveSpeed = dissolveSpeed;
    }
}
