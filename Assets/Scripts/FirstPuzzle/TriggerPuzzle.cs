using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerPuzzle : MonoBehaviour
{

    public GameObject puzzleGameobject;

    public static Action changeCamState;

    public ChangeTipStatus tip;

    bool inPuzzleZone = false;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inPuzzleZone = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inPuzzleZone = true;
            tip.ChangeTipState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inPuzzleZone = false;
            DeactivatePuzzle();
            tip.ChangeTipState(false);
        }
    }

    private void Update()
    {
        if (inPuzzleZone) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(puzzleGameobject.activeSelf);
                if (!puzzleGameobject.activeSelf)
                    ActivatePuzzle();
                else 
                {
                    DeactivatePuzzle();
                    tip.ChangeTipState(true);
                }
                    
            }
        }
    }

    private void OnDisable()
    {
        tip.ChangeTipState(false);
        DeactivatePuzzle();
    }


    public void DisableSelf() 
    {
        transform.gameObject.SetActive(false);
    }

    void ActivatePuzzle() 
    {
        puzzleGameobject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        changeCamState?.Invoke();
        tip.ChangeTipState(false);

    }

    void DeactivatePuzzle() 
    {
        if (puzzleGameobject.activeSelf) 
        {
            puzzleGameobject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            changeCamState?.Invoke();
        }
    }
}
