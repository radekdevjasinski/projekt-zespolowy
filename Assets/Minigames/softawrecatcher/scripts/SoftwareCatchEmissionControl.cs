using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Bson;
using UnityEngine;

public class SoftwareCatchEmissionControl : MonoBehaviour, MinionBoss
{
    [SerializeField] private GameObject emssionObject;
    [SerializeField] private float emissoinRate=1;
    [SerializeField] private bool emissionActive=false;
    [SerializeField] private float emitterWidth=10;
    [SerializeField] private float sppedUpRate = 0.01f;

    private Queue<string> filenamesTobeintanced=new Queue<string>();
    private Queue<string> previousFileNames = new Queue<string>();

    private PlayerEntityController playerEntityController;
    private Mutex mutex=new Mutex();
    private void Awake()
    {
        playerEntityController=GetComponent<PlayerEntityController>();
        try
        {
            filenamesTobeintanced =
                new Queue<string>(IconExtractor.getAllSoftwareNamesFile().OrderBy(_ => UnityEngine.Random.value));
        }
        catch (Exception e)
        {
            Debug.Log("For soem reason coudltn rettrice software names: "+ e.Message);
        }

        if (filenamesTobeintanced.Count == 0)
        {
            filenamesTobeintanced.Enqueue("example file, need to be fileed");
        }
    }

    public void activateEmission()
    {
        emissionActive=true;
        emit();
    }

    public void onMinonDeath()
    {
        playerEntityController.dealDamage(1);
    }

    private void emit()
    {
        this.emissoinRate += sppedUpRate;
        mutex.WaitOne();
        if (filenamesTobeintanced.Count <= 0)
        {
            filenamesTobeintanced = previousFileNames;
            previousFileNames.Clear();
        }
        GameObject obj = Instantiate(emssionObject, transform);
        obj.transform.localPosition = new Vector3(UnityEngine.Random.Range(-emitterWidth,emitterWidth),0,0);
        string newFileName = filenamesTobeintanced.Dequeue();
        previousFileNames.Enqueue(newFileName);
        mutex.ReleaseMutex();
        obj.GetComponent<TextSetter>().setText(newFileName);
        obj.AddComponent<InformBossAboutDeath>().setMinionBoss(this);
        if(emissionActive&& emissoinRate>0)
        Invoke("emit",1/emissoinRate);
    }

    private void Start()
    {
        activateEmission();
    }
}
