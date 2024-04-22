using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichRoomStartSequance : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //Debug.Log("Lich Boss Fight Sequance Start");
         animator.GetComponent<BossFightController>().attachCameraToBossRoomCamera();
         animator.GetComponent<BossFightController>().lockPlayer();
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Lich Boss Fight Sequance Start stop");
        animator.GetComponent<BossFightController>().deattachCameraToBossRoomCamera();
        animator.GetComponent<BossFightController>().unlockPlayer();
    }

    
}
