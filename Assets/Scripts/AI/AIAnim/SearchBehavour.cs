using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchBehavour : StateMachineBehaviour
{
    float timer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0; 
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        timer += Time.deltaTime;
        if (timer > 10)
        {
            animator.SetBool("IsSearching", false);
            animator.SetBool("IsWalking", true);
            animator.Play("walking");
            timer = 0;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //ai_nav.SetDestination(ai_nav.transform.position);
    }
}
