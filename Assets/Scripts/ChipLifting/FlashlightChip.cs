using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightChip : MonoBehaviour
{
    [SerializeField]
    private GameObject cam;

    Rigidbody currentItemInHands;

    private bool in_zone = false;

    public static Action FlashlightTaken;

    public GameObject trigger_active1;

    public GameObject trigger_active2;

    public static Action SetPlayerMovementDisabled;

    void Update()
    {
        checkPlayersItem();
        if (currentItemInHands != null)
        {
            if (in_zone && currentItemInHands.gameObject.tag == "Flashlight_chip" && Input.GetKeyDown(KeyCode.E))
            {
                FlashlightTaken?.Invoke();
                currentItemInHands.gameObject.SetActive(false);

            }
            else if (in_zone && currentItemInHands.gameObject.tag == "Voice_chip" && Input.GetKeyDown(KeyCode.E))
            {
                currentItemInHands.gameObject.SetActive(false);
            }else if (in_zone && currentItemInHands.gameObject.tag == "Memory_chip" && Input.GetKeyDown(KeyCode.E))
            {
                //здесь катсцена
                SetPlayerMovementDisabled?.Invoke();
                currentItemInHands.gameObject.SetActive(false);
                OnQuestIsDoneDialog.the_end_dialoge = true;
                trigger_active1.SetActive(true);
                AIBehavuor.is_the_end = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            in_zone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            in_zone = false;
        }
    }

    void checkPlayersItem()
    {
        var temp = cam.GetComponent<HoldNDrop>();
        currentItemInHands = temp.currentlyHoldItem_;
    }

}
 