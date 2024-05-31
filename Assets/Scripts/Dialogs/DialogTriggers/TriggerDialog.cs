using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
