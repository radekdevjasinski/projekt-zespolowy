using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]

public class SmokeLoadinStop : MonoBehaviour
{
    void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    public void OnParticleSystemStopped()
    {
        Debug.Log("Particle system stop");
        this.GetComponentInParent<LichWarriorEntity>().deathLoaded();
        Destroy(this.gameObject);
    }
}
