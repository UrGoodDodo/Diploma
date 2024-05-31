using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonPressWithItemDialog : MonoBehaviour
{
    Rigidbody rb;

    public HoldNDrop HnDSystem;

    public GameObject itemToCheck;

    private void Start()
    {
        cd = GetComponent<LinearDialog>();
    }

    LinearDialog cd;

    bool activateDialog = false;

    bool playerInArea = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            playerInArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = false;
        }
    }

    private void Update()
    {
        rb = HnDSystem.currentlyHoldItem_;
        if (!activateDialog) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                if (cd != null)
                {
                    
                    if (rb != null) 
                    {
                        
                        if (!DialogCore.dialogsAreActive && playerInArea && rb.gameObject == itemToCheck)
                        {
                            Debug.Log("Lf");
                            cd.clauseFullFilled();
                            activateDialog = true;
                        }
                    }
                }
            }
            
        }
    }
}
