using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyQuest : MonoBehaviour
{

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private GameObject key;

    Rigidbody currentItemInHands;

    bool playerInArea = false;

    bool questIsDone = false;

    public delegate void questComplete(int num);
    public static event questComplete questCompleteEvent;

    public static Action KeyIsFound;

    void FixedUpdate()
    {
        if (!questIsDone)
        {
            if (currentItemInHands != null) 
            {
                if (currentItemInHands.gameObject == key)
                    KeyIsFound?.Invoke();

                if (playerInArea && currentItemInHands.gameObject == key && Input.GetKey(KeyCode.F))
                {
                    questIsDone = true;
                    questCompleteEvent?.Invoke(1);
                    currentItemInHands.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            checkPlayersItem();
            playerInArea = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInArea = false;
        }
    }


    void checkPlayersItem() 
    {
        var temp = cam.GetComponent<HoldNDrop>();
        currentItemInHands = temp.currentlyHoldItem_;
    }
}
