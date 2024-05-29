using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightChip : MonoBehaviour
{
    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private GameObject chip;

    Rigidbody currentItemInHands;

    //
    public static Action FlashlightChipTaken;

    void FixedUpdate()
    {
        if (currentItemInHands != null)
        {
            if (currentItemInHands.gameObject == chip && Input.GetKey(KeyCode.F))
            {

                FlashlightChipTaken?.Invoke();
                currentItemInHands.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            checkPlayersItem();
        }
       // playerInArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
       // playerInArea = false;
    }


    void checkPlayersItem()
    {
        var temp = cam.GetComponent<HoldNDrop>();
        currentItemInHands = temp.currentlyHoldItem_;
    }
}
