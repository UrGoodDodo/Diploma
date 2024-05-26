using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerR2Behavour : MonoBehaviour
{
    public static bool IsTriggeredR2;
    public static Action StartBookTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (IsTriggeredR2)
            {
                StartBookTrigger?.Invoke();
            }
        }
    }
}
