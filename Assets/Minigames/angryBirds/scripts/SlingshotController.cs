using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlingshotController : MonoBehaviour
{

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileOrigin;
    [SerializeField] private int projectilesCounter=4;
    [SerializeField] private Transform throwObject;
    [SerializeField] private float timeBetwenreload = 1;
    private SlingShototerActionContrrller throwSligh;
    private float spaceBetwwenProejctile=2;
    private Queue<GameObject> ammo = new Queue<GameObject>();
    [SerializeField]
    private int aliveProejtile;
    private void Awake()
    {
        aliveProejtile = projectilesCounter;
        for (int i = 0; i < projectilesCounter; i++)
        {
            GameObject amuni = Instantiate(projectilePrefab,
                projectileOrigin.position - new Vector3(spaceBetwwenProejctile * i, 0, 0), new Quaternion(),
                projectileOrigin);
            amuni.AddComponent<InformShootweAboutDeath>().setSlingShotContrler(this);
                
            ammo.Enqueue(amuni);
        }

        throwSligh = throwObject.GetOrAddComponent<SlingShototerActionContrrller>();
        reload();
    }

    public void reload()
    {
        throwSligh.setIsReloadin(true);
        if(ammo.Count>0)
        StartCoroutine("moveReload");

    }

    public void decreaseNumberOfAliveProjetile()
    {
        Debug.Log("decrase alive proceitls");
        aliveProejtile--;
        if (aliveProejtile <= 0)
        {
            GetComponentInParent<AngrybirdLevelController>().lostGame();
        }
    }

    private IEnumerator moveReload()
    {
        GameObject ammunaiton = ammo.Dequeue();
        Vector3 startpos = ammunaiton.transform.position;
        float time = 0;
        float amoMoveOfset = spaceBetwwenProejctile / timeBetwenreload;
        while (time< timeBetwenreload)
        {
            time += Time.deltaTime;
            ammunaiton.transform.position = Vector3.Lerp(startpos, throwObject.position, time/ timeBetwenreload);
            foreach (GameObject am in ammo)
            {
                am.transform.position= am.transform.position+ Time.deltaTime* new Vector3(amoMoveOfset,0,0);
            }
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("setTransorm");

        ammunaiton.transform.SetParent(throwObject.transform);
   
        throwSligh.setIsReloadin(false);
    }

}
