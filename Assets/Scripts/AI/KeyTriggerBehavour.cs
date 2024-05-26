using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTriggerBehavour : MonoBehaviour
{
    public static bool StartToSearch;
    public Transform ai;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("In Zone");
        if(other.tag == "Player" && StartToSearch)
        {
            Debug.Log("Start Searching");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Not in Zone");
    }
}
