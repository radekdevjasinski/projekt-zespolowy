using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAnimatedAffect : MaterialAffect
{
    [SerializeField] private float minVAl;
    [SerializeField] private float maxVAl;
    [SerializeField] private string animationValueName;


    protected override void affect()
    {
        base.affect();
        StartCoroutine("aniamteMat");
    }

    IEnumerator aniamteMat()
    {
        Debug.Log("animte");
        float value = minVAl;
        float time = 0;
        while (value < maxVAl)
        {
            newMat.SetFloat(animationValueName,value);
            Debug.Log("setting new value animaiton to " + value);
            yield return new WaitForEndOfFrame();
            time+= Time.deltaTime;
            value = Mathf.Lerp(minVAl, maxVAl, time / this.timeOfAffect);

        }
    }



}
