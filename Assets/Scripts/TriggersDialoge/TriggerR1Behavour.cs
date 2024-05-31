using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriggerR1Behavour : MonoBehaviour
{
    public static bool IsTriggeredR1 = false;
    public static Action StartedKeyTrigger;
         
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(!IsTriggeredR1 && !AIBehavuor.key_was_found && LightBehavour.flashlight_was_taken)
            {
                StartedKeyTrigger?.Invoke();
            }
        }
            
    }
}
