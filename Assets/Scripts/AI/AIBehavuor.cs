using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehavuor : MonoBehaviour
{

    //Component of AI navigation
    public NavMeshAgent ai;
    //Component of player
    public Transform player;
    //Player`s position
    Vector3 dest;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
        
    }

    //State of following AI
    protected void Follow()
    {
        dest = player.position;
        ai.destination = dest;
    }

}
