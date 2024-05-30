using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkBehavour : StateMachineBehaviour
{
    float timer;

    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (AIBehavuor.is_searching_key && !AIBehavuor.key_was_found)
        {
            if (AIBehavuor.ai_nav.remainingDistance <= AIBehavuor.ai_nav.stoppingDistance)
            {
                AIBehavuor.ai_nav.SetDestination(AIBehavuor.points[Random.Range(0, AIBehavuor.points.Count)].position);
            }
            timer += Time.deltaTime;
            if (timer > 5)
            {
                animator.SetBool("IsSearching", true);
                animator.SetBool("IsWalking", false);
                animator.Play("search");
                timer = 0;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
