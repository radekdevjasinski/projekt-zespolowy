using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichBossStartSequance : MonoBehaviour
{
    [Header("Prefabs")] 
    [SerializeField] private GameObject crystal;
    [SerializeField] private GameObject Boss;
    [SerializeField] private GameObject Shield;
    [Header("Time offset")]
    [SerializeField] private float cystalAppearTimeOffset;
    [SerializeField] private float bossSummonTimeOffset;
    [SerializeField] private float shieldSummonTimeOffset;

    [Header("postions")] [SerializeField] private Transform bossSummonPostion;
    [SerializeField] private Transform cystalSummonPosition;

    private Transform summonedCrystal;

    public void startSequance()
    {
        Debug.Log("startSequance");
        Invoke("summonCrystal", cystalAppearTimeOffset);
        Invoke("summonBoss",  bossSummonTimeOffset);
        Invoke("summonShield", shieldSummonTimeOffset);
    }

    void summonCrystal()
    {
        summonedCrystal = Instantiate(crystal, cystalSummonPosition,true).transform;
    }
    void summonBoss()
    {
        Instantiate(Boss, bossSummonPostion);

    }

    void summonShield()
    {
        Instantiate(Shield,  summonedCrystal.Find("shieldPosition").transform);
    }

    public void startSequanceImidielty()
    {
        summonCrystal();
        summonBoss();
        summonShield();
    }
}
