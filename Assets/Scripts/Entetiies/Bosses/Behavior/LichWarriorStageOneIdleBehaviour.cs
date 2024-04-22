using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LichWarriorStageOneIdleBehaviour : StateMachineBehaviour
{
     //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        private const string AnimatorSpawiningZombieState= "IsSpawningZombies";

        private Animator animator;

     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Stage one idle Enter");
        this.animator=animator;
        if(!animator.GetComponent<LichWarriorEntity>().getIsWaiting())
            goToSpawingZombie();


    }

    private void goToSpawingZombie()
    {
        //Debug.Log("go to spawing ing zombie: "+ animator.GetBool(AnimatorSpawiningZombieState));
        animator.SetBool(AnimatorSpawiningZombieState,true);
        //Debug.Log(" zombie: " + animator.GetBool(AnimatorSpawiningZombieState));
    }

    

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
