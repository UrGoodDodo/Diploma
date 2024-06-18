using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDialog : MonoBehaviour
{

    LinearDialog ld;
    NonLinearDialog nld;

    private void Start()
    {
        ld = GetComponent<LinearDialog>();
        nld = GetComponent<NonLinearDialog>();
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (ld != null && nld == null)
        {
            if (!DialogCore.dialogsAreActive && ld.GotDialog && other.CompareTag("Player"))
            {
                ld.clauseFullFilled();
                if(SceneManager.GetActiveScene().buildIndex == 0 && !AIBehavuor.is_start)
                {
                    AIBehavuor.is_start = true;
                } 
                //if (SceneManager.GetActiveScene().buildIndex == 1 && RoomMovement.dog_room == 2)
                //{
                //    AIBehavuor.is_start = true;
                //}
            }
        }
        else if (ld == null && nld != null) 
        {
            if (!DialogCore.dialogsAreActive && other.CompareTag("Player"))
            {
                nld.clauseFullFilled();
            }
        }
        
    }
}
