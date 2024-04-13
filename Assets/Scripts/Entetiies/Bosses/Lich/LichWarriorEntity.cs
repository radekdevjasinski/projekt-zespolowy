using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LichWarriorEntity : EntityController<float>, MinionBoss, StageDeprndentElements
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
        bossFightController.addStageDependentElement(this);
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

    public void summonShield()
    {
        this.bossFightController.summonShield();
    }


    #region StageOneBehaviour
    [Header("Zombie Stage 1")]
    [SerializeField] private float delayBetweenMinionSpawn;
    [SerializeField] private int minionToSpawn;
    [SerializeField] private GameObject minionSummon;
    [SerializeField] private float sleepTime;
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
  
  currentAmountOfMinons--;
  Debug.Log("Lich minion Death: "+ currentAmountOfMinons);
        if (currentAmountOfMinons <= 0)
        {
            goSleep();
}
    }

    private void goSleep()
    {
        Debug.Log("GoSleep");
        bossFightController.destroyShield();
        this.animator.SetBool("IsSleeping", true);
        Invoke("wakeUp", sleepTime);
    }

    private void wakeUp()
    {
        this.isWaiting = false;
        this.animator.SetBool("IsSleeping", false);
    }


    #endregion






    #region Stage2 Behaviour

    [Header("Movmeent")] [SerializeField] private float timeToMoveBetwwenPoints;
    [Header("Stage 2 attacks")]
    [SerializeField] 
    private int actionsPerBehaviour=0;
    [SerializeField]
    private int BehaviourPerPhaze = 0;
    [SerializeField]
    private float ActionCoolDown = 0;
    private Vector2 targetPositon = new Vector2();
    private Vector2 velocity=Vector2.zero;

    private int currentAction = 0;
    private int currentBehavioutNumber = 0;

    private string[] possibleAttackPhazes = { "IsShootingAround", "IsInDropAttack" };

    private string getRandomBehaviour()
    {
        return possibleAttackPhazes[Random.Range(0, possibleAttackPhazes.Length)];
    }


    private string currentActiveBehaviour= "IsShootingAround";



    private IEnumerator moveToPostion()
    {
        float currMovementTime = 0.0f;
        Vector2 orgin = transform.position;
        while (Vector2.Distance(transform.position, targetPositon) > 0.01)
        {
            currMovementTime += Time.deltaTime;
            transform.position = Vector2.SmoothDamp(transform.position, targetPositon,ref velocity, timeToMoveBetwwenPoints);
            //Debug.Log("postion: "+ transform.position+"Distane: "+ Vector2.Distance(transform.localPosition, targetPositon));
            yield return null;
        }

        performActiofBehaviout();
    }

    void performActiofBehaviout()
    {
        Debug.Log("Perfomr action: "+ currentActiveBehaviour);
        animator.SetBool(currentActiveBehaviour,true);
     
        //  animator.ResetTrigger(currentActiveBehaviour);
    }

    public void perfromStage2Action()
    {
        Debug.Log("perform Stage 2 action");
        animator.SetBool(currentActiveBehaviour, false);
        currentAction++;
        Debug.Log("perform Stage 2 action");
        if (currentAction > this.actionsPerBehaviour)
        {
            this.currentBehavioutNumber++;
         
            currentActiveBehaviour = getRandomBehaviour();
            currentAction = 1;
            if (currentBehavioutNumber >= BehaviourPerPhaze)
            {
                this.goSleep();
                currentAction = 0;
                currentBehavioutNumber = 0;
                return;
            }
        }
        

        this.targetPositon = this.bossFightController.getRandomPostionOnBossMap(1);
        //Debug.Log("Moving towards: "+ targetPositon);
        Invoke("move", ActionCoolDown);

    }

    public void move()
    {
        StartCoroutine(moveToPostion());
    }

    public void dropAttack()
    {
        Debug.Log("Drop attack");
    }

    public void shootAttack()
    {
        Debug.Log("shoot attack");
    }




    #endregion


    #region Stage3 AdjustmentSpeed

    [Header("Stage attrbiutes adjustments")]
    [SerializeField]
    private float newMovemntTime;
    [SerializeField]
    private float newCooldown;
    [SerializeField]
    private int newAmountOfActionPerBehaviour;
    [SerializeField]
    private int newAmountBehavioutPerPhases;
    [SerializeField]
    private float newSleepTime;

    private void adjustParamtersForStage3()
    {
        Debug.Log("Adjust for stage3");
        this.timeToMoveBetwwenPoints = newMovemntTime;
        this.ActionCoolDown = newCooldown;
        this.actionsPerBehaviour = newAmountOfActionPerBehaviour;
        this.BehaviourPerPhaze = newAmountBehavioutPerPhases;
        this.sleepTime=newSleepTime;
        this.currentAction = 0;
        this.currentBehavioutNumber = 0;
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


    public void increaseStage()
    {
    
      animator.SetInteger("Stage",this.animator.GetInteger("Stage")+1);
      if (this.animator.GetInteger("Stage") == 2)
          adjustParamtersForStage3();
      Debug.Log("Lich Stage increase: " + this.animator.GetInteger("Stage"));
    }

 
}
