using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    

    private void Awake()
    {
        this.BossFightRoomAniamtor = this.GetComponent<Animator>();
        activeCamera = this.activeCamera;
        lichBossStartSequance = this.GetComponent<LichBossStartSequance>();
        Player = GameObject.Find("Player");
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

    #endregion



}
