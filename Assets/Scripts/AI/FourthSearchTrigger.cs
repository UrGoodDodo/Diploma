using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthSearchTrigger : MonoBehaviour
{
    public int trigger_num;

    private void OnTriggerEnter(Collider other)
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            AIBehavuor.points.Clear();
            if (trigger_num == 1)
            {
                AIBehavuor.search_number = 1;
            }else if (trigger_num == 2)
            {
                AIBehavuor.search_number = 2;
            }
            else if(trigger_num == 3)
            {
                AIBehavuor.search_number = 3;
            }
            AIBehavuor.flag_restart = true;
            AIBehavuor.is_searching_key = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {

            AIBehavuor.is_searching_key = false;
        }
    }
}
