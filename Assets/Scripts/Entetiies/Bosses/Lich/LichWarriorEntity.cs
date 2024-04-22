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
    private Transform ObjectHolder;
    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        this.minionsParent = GameObject.Find("Minions").transform;
        bossFightController = GetComponentInParent<BossFightController>();
        bossFightController.addStageDependentElement(this);
        ObjectHolder = transform.Find("ObjectHolder");
    }
    #region BossActions

    public void rage()
    {
        SoundManager.instance.playSound(this.transform,rageSound);
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
    [SerializeField] private float timeToSummonMinon;
    [SerializeField] private int minionToSpawn;
    [SerializeField] private GameObject minionSummon;
    [SerializeField] private float sleepTime;
    private int currentAmountOfMinons = 0;


    public void StartZombieSummon()
    {
        //Debug.Log("Start Zobmie SUmmon");
        float delayBetweenMinionSpawn = timeToSummonMinon / minionToSpawn;
        for (int i = 0; i < minionToSpawn; i++)
        {
            Invoke("summonMinion",delayBetweenMinionSpawn*i);
        }
        Invoke("stopZombieSummon", timeToSummonMinon);

    }

    public void stopZombieSummon()
    {
        isWaiting = true;
        animator.SetBool("IsSpawningZombies",false);
    }

    

    private void summonMinion()
    {
        currentAmountOfMinons++;
        Vector2 pos= bossFightController.getRandomPostionOnBossMap(0.1f);
        GameObject minion = Instantiate(this.minionSummon, new Vector3(pos.x, pos.y,0) , new Quaternion(), this.minionsParent);
        //Debug.Log("Set mionn bsses");
        foreach (InformBossAboutDeath VARIABLE in minion.GetComponentsInChildren<InformBossAboutDeath>())
        {
            //Debug.Log("Set mionn boss for : "+ VARIABLE.name);
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
        //bossFightController.destroyShield();
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
    //[SerializeField]
    //private int BehaviourPerPhaze = 0;
    [SerializeField] private int minonsToSpawnOnStage2=3;
    [SerializeField]
    private float ActionCoolDown = 0;

    [SerializeField] private GameObject LichDropBurst;
    [SerializeField] private GameObject LichProejctile;
    [SerializeField] private int projectileDmg = 1;
    [SerializeField] private float ProejctileSpeed = 10;
    [SerializeField] private float timeOfRoation;
    [SerializeField] private int amtOfPoejcitles = 10;
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
        //Debug.Log("Perfomr action: "+ currentActiveBehaviour);
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
            //if (currentBehavioutNumber >= BehaviourPerPhaze)
            //{
            //    this.goSleep();
            //    currentAction = 0;
            //    currentBehavioutNumber = 0;
            //    return;
            //}
        }
        

        this.targetPositon = this.bossFightController.getRandomPostionArundPlayer(1,3);
        //Debug.Log("Moving towards: "+ targetPositon);
        Invoke("move", ActionCoolDown);

    }

    public void move()
    {
        StartCoroutine(moveToPostion());
    }

    public void dropAttack()
    {
        Debug.Log("Drop Attack");
        Instantiate(this.LichDropBurst,transform.position,new Quaternion(), transform.parent);
    }


    public void shootAttack()
    {
        Debug.Log("shoot attack");
        for (int i = 0; i < amtOfPoejcitles; i++)
        {
            StartCoroutine(shoot(timeOfRoation / amtOfPoejcitles * i, i));
         
        }

     
    }
    private void adjustParamtersForStage2()
    {
        this.minionToSpawn = minonsToSpawnOnStage2;

    }

    private IEnumerator shoot(float time,int numbber)
    {
        yield return new WaitForSeconds(time);

        float angle = 360/amtOfPoejcitles* numbber * Mathf.Deg2Rad;
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);


        Vector2 dir = new Vector2(x,y);
        //Debug.Log("Direction: " + ObjectHolder.rotation);
        Vector3 startPosition = this.transform.position + new Vector3(dir.x,dir.y,0)*2;
                ;
            GameObject shootable = Instantiate(
                this.LichProejctile,
                startPosition,
                transform.rotation,
                transform.parent
            );
            shootable.GetComponent<Projectile>().setupProjectileParams(
                this.projectileDmg,
                1
            );
            
            shootable.GetComponent<Projectile>().setWasShootByPlayer(false);
            shootable.GetComponent<Rigidbody2D>().AddForce(dir * ProejctileSpeed , ForceMode2D.Impulse);

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
    //[SerializeField]
    //private int newAmountBehavioutPerPhases;
    [SerializeField]
    private float newSleepTime;
    [SerializeField] private int minonsToSpawnOnStage3 = 3;
    private void adjustParamtersForStage3()
    {
        Debug.Log("Adjust for stage3");
        this.timeToMoveBetwwenPoints = newMovemntTime;
        this.ActionCoolDown = newCooldown;
        this.actionsPerBehaviour = newAmountOfActionPerBehaviour;
        //this.BehaviourPerPhaze = newAmountBehavioutPerPhases;
        this.minionToSpawn = minonsToSpawnOnStage3;
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
      
       
    }

    public void destroy()
    {
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

    public override void reviceDamage(float damage)
    {
      // this entity does not recive dagme
    }


    public void increaseStage()
    {
    
      animator.SetInteger("Stage",this.animator.GetInteger("Stage")+1);
      if (this.animator.GetInteger("Stage") == 1)
          adjustParamtersForStage2();
        if (this.animator.GetInteger("Stage") == 2)
          adjustParamtersForStage3();
      Debug.Log("Lich Stage increase: " + this.animator.GetInteger("Stage"));
    }


    public void destroyShield()
    {
      bossFightController.destroyShield();
    }
}
