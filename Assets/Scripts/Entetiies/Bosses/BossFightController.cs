using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossFightController : MonoBehaviour, StageDeprndentElements
{

    //Todo: changing timing of appreance to animation events

    [SerializeField] private Transform cameraFollowPoint;
    [SerializeField] private bool skipStartSequnace=false;
    [SerializeField] private GameObject battleAmbient;



   
    private Animator BossFightRoomAniamtor;
    private Camera activeCamera;
    private GameObject Player;
    private LichBossStartSequance lichBossStartSequance;
    private Collider2D fightMap;

    private void Awake()
    {
      
        this.BossFightRoomAniamtor = this.GetComponent<Animator>();
        activeCamera = this.activeCamera;
        lichBossStartSequance = this.GetComponent<LichBossStartSequance>();
        Player = GameObject.Find("Player");
        fightMap = transform.Find("FightMap").GetComponent<Collider2D>();
    }




    public void startFight()
    {
        Debug.Log("Start Fight");
       if(battleAmbient!=null)
           SoundManager.instance.setAmbient(battleAmbient);
        if(skipStartSequnace)
            lichBossStartSequance.startSequanceImidielty();
        else
        {
            this.BossFightRoomAniamtor.SetBool("duringFight", true);
            lichBossStartSequance.startSequance();
        }
    }


    public void stopFight()
    {
        this.BossFightRoomAniamtor.SetBool("duringFight", false);
        SoundManager.instance.revertToBasicAmbient();
        transform.Find("Boss").GetComponentInChildren<LichWarriorEntity>().dealDamage(1);
    }


    #region CameraControls

    private Transform previousCameraAttachment;
    public void attachCameraToBossRoomCamera()
    {
        previousCameraAttachment = Camera.main.GetComponent<CameraFollow>().getFollowPoint();
        this.cameraFollowPoint.transform.position = previousCameraAttachment.position;
        Camera.main.GetComponent<CameraFollow>().setFollowPoint(this.cameraFollowPoint);
    }

    public void deattachCameraToBossRoomCamera()
    {
        Camera.main.GetComponent<CameraFollow>().setFollowPoint(previousCameraAttachment);
    }
    #endregion


    public void lockPlayer()
    {
        Player.GetComponent<PlayerControler>().lockInput() ;
    }

    public void unlockPlayer()
    {
        Player.GetComponent<PlayerControler>().unlockInput();
    }


    public void summonShield()
    {
        this.lichBossStartSequance.summonShield();
    }

    public void destroyShield()
    {
       Destroy(this.lichBossStartSequance.getSummoneShield().gameObject);
    }


    #region Stages
    [SerializeField] private int[] stageBarriers;
    private int activeStage=0;
    private List<StageDeprndentElements> stageDependentElements=new List<StageDeprndentElements>();

    public void increaseStage()
    {
      
        this.activeStage += 1;
        Debug.Log("Increase stage to: " + activeStage);
        foreach (StageDeprndentElements var in stageDependentElements)
        {
            var.increaseStage();
        }
    }
    public int getNextStageBarrier()
    {
        Debug.Log("getting next barrier: "+ activeStage);
        if (activeStage >= stageBarriers.Length)
            return -1;
        return stageBarriers[activeStage];
    }

    public void addStageDependentElement(StageDeprndentElements var)
    {
        Debug.Log("add stage depende elelmt: ");
        this.stageDependentElements.Add(var);
    }

    public int getStage()
    {
        return activeStage;
    }

    private Vector2 getPostionInBoudneries(Bounds bounds)
    {

        Vector2 scaledPOstion= new Vector2(Random.Range(bounds.min.x*1000, bounds.max.x*1000), Random.Range(bounds.min.y*1000.0f, bounds.max.y*1000.0f));
        return scaledPOstion / 1000.0f;
    }

    internal Vector2 getRandomPostionOnBossMap(float collisionRadius)
    {
        int attempts = 20;
        bool state = false;
        Collider2D[] res;
        Vector2 pos=new Vector2();
        while (state==false) {
            Random.seed = System.DateTime.Now.Millisecond* attempts;

            if (attempts--<20)
                break;
        pos = getPostionInBoudneries(this.fightMap.bounds);
        //Debug.Log("Positon to cosisder: "+ pos);
        res = Physics2D.OverlapCircleAll(pos, collisionRadius);
        //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), pos, new Quaternion());
        foreach (Collider2D var in res)
        {
            if (var.CompareTag("Map"))
            {
                Debug.Log("Correct collsion wih: " + var.name);

                    state = true;

            }
            else
            {
                Debug.Log("False collsion wih: " + var.name);

                    state = false;
                break;
            }
        }

        }

        return pos;


    }

    #endregion



}
