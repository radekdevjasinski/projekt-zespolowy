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
    private Transform summonedShield;

    public void startSequance()
    {
        Debug.Log("startSequance");
        Invoke("summonCrystal", cystalAppearTimeOffset);
        Invoke("summonBoss",  bossSummonTimeOffset);
        Invoke("summonShield", shieldSummonTimeOffset);
    }

    public void summonCrystal()
    {
        summonedCrystal = Instantiate(crystal, cystalSummonPosition,false).transform;
    }
    void summonBoss()
    {
       Instantiate(Boss, bossSummonPostion);
    }

    public void summonShield()
    {
        if (summonedCrystal != null && summonedShield==null)
        {
            
            summonedShield = Instantiate(Shield, summonedCrystal.Find("shieldPosition").transform).transform;
        }
    }

    public void startSequanceImidielty()
    {
        summonCrystal();
        summonBoss();
        summonShield();
    }


    public Transform getSummoneShield()
    {
        return summonedShield;
    }
    public Transform getSummonedCrystal()
    {
        return summonedCrystal;
    }

}
