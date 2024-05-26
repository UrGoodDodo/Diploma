using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchKeyBehavour : StateMachineBehaviour
{

    float timer;
    List<Transform> points = new List<Transform>();
    NavMeshAgent ai_nav;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        Transform pointObject = GameObject.FindGameObjectWithTag("Points").transform;
        foreach(Transform p in pointObject)
        {
            points.Add(p);
        }

        ai_nav.SetDestination(points[0].position);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(ai_nav.remainingDistance <= ai_nav.stoppingDistance)
        {
            ai_nav.SetDestination(points[Random.Range(0, points.Count)].position);
        }

        timer += Time.deltaTime;
        if(timer > 10)
        {

        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai_nav.SetDestination(ai_nav.transform.position);
    }
}
