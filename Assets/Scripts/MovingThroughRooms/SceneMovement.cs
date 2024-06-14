using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMovement : MonoBehaviour
{
    bool inTriggerZone = false;

    static bool canMove = true;

    public bool forwardDirection = true;

    public static Action movedToAnotherScene;

    public delegate void moveType(bool flag);
    public static event moveType moveTypeEvent;

    private int curScene;

    public ChangeTipStatus tip;

    private void Start()
    {
        curScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTriggerZone = true;
            tip.ChangeTipState(true);
        }

    }

    void Update()
    {
        if (inTriggerZone && canMove)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (forwardDirection)
                    SwitchScene(1);
                else
                    SwitchScene(-1);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTriggerZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTriggerZone = false;
            tip.ChangeTipState(false);
        }
    }

    void SwitchScene(int num)
    {
        if (num > 0)
        {
            movedToAnotherScene?.Invoke();
            moveTypeEvent?.Invoke(true);
            SceneManager.LoadScene(curScene + num);
        }
        else
        {
            moveTypeEvent?.Invoke(false);
            SceneManager.LoadScene(curScene + num);
        }
    }
}
