using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightController : MonoBehaviour
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
}
