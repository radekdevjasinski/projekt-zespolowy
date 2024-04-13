using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichWarriorEntity : EntityController<float>, MinionBoss
{

    [SerializeField] private GameObject particleDeathLoading;

    [SerializeField] private GameObject rageSound;

    private BossFightController bossFightController;
    private Animator animator;
    private Transform minionsParent;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        this.minionsParent = GameObject.Find("Minions").transform;
        bossFightController = GetComponentInParent<BossFightController>();
    }
    #region BossActions

    public void rage()
    {
        SoundManager.instance.playSound(this.transform,rageSound,new Vector3(0,0,0));
    }


    #region BehAviours

    private bool isWaiting = false;

    public bool getIsWaiting()
    {
        return isWaiting;

    }




    #region StageOneBehaviour
    [Header("Zombie Stage 1")]
    [SerializeField] private float delayBetweenMinionSpawn;
    [SerializeField] private int minionToSpawn;
    [SerializeField] private GameObject minionSummon;
    private int currentAmountOfMinons = 0;


    public void StartZombieSummon()
    {
        Debug.Log("Start Zobmie SUmmon");
        for (int i = 0; i < minionToSpawn; i++)
        {
            Invoke("summonMinion",delayBetweenMinionSpawn*i);
        }
        Invoke("stopZombieSummon", minionToSpawn * delayBetweenMinionSpawn);

    }

    public void stopZombieSummon()
    {
        isWaiting = true;
        animator.SetBool("IsSpawningZombies",false);
    }

    

    private void summonMinion()
    {
        currentAmountOfMinons++;
        Vector2 pos= bossFightController.getRandomPostionOnBossMap(5);
        GameObject minion = Instantiate(this.minionSummon, new Vector3(pos.x, pos.y,0) , new Quaternion(), this.minionsParent);
        Debug.Log("Set mionn bsses");
        foreach (InformBossAboutDeath VARIABLE in minion.GetComponentsInChildren<InformBossAboutDeath>())
        {
            Debug.Log("Set mionn boss for : "+ VARIABLE.name);
            VARIABLE.setMinionBoss(this);
        }
    }
    public void onMinonDeath()
    {
  Debug.Log("Lich minion Death");
  currentAmountOfMinons--;
    }


    #endregion



    #endregion


    #endregion

    protected override void onDie()
    {
        Debug.Log(" animator.SetTrigger(\"LoadDeath\");");
        animator.SetTrigger("LoadDeath");
    }

    public void deathLoad()
    {
        Debug.Log("Death loading");
        Instantiate(particleDeathLoading, this.transform, false);
    }

    public void deathLoaded()
    {
        runDeathSequanceElemnts();
        Destroy(this.gameObject);
    }
    protected override float getHealth()
    {
        return 0;
    }

    protected override float getMaxHealth()
    {
        return 1;
    }

    protected override void reviceDamage(float damage)
    {
      // this entity does not recive dagme
    }

    


 
}
