using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIdleState : StateMachineBehaviour
{
    AttackController attackController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       attackController = animator.transform.GetComponent<AttackController>();
       attackController.SetIdleMaterial();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Checks for an available target
        animator.SetBool("isIdle", true);
        if (attackController.targetToAttack != null)
        {
            //Transitions to the follow state
            animator.SetBool("isFollowing", true);
        }
       
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isIdle", false);
    }
}
