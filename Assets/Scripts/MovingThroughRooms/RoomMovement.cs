using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class RoomMovement : MonoBehaviour
{
    //public GameObject tip;

    public bool forwardDirection = true;

    public int curRoom = 1;

    public GameObject positionToMove;

    Collider otherCollider;

    bool triggerActive = false;

    static bool canMove = true;

    bool FquestIsDone = false;

    bool SquestIsDone = false;


    private void OnEnable()
    {
        KeyQuest.questCompleteEvent += setActiveDoor;
        CoreBookQuest.questCompleteEvent += setActiveDoor;
    }

    private void OnDisable()
    {
        KeyQuest.questCompleteEvent -= setActiveDoor;
        CoreBookQuest.questCompleteEvent -= setActiveDoor;
    }

    private void OnTriggerEnter(Collider other)
    {
        // tip.SetActive(true);
        triggerActive = true;
        otherCollider = other;
    }
    void FixedUpdate() 
    {
        if (triggerActive && canMove) 
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (forwardDirection)
                {
                    if (curRoom == 1 && FquestIsDone) 
                    {
                        MovePlayer(otherCollider, positionToMove);
                        StartCoroutine(Wait());
                    }
                    if (curRoom == 2 && SquestIsDone)
                    {
                        MovePlayer(otherCollider, positionToMove);
                        StartCoroutine(Wait());
                    }
                }
                else
                {
                    MovePlayer(otherCollider, positionToMove);
                    StartCoroutine(Wait());
                }

            }
        }
    }

    private void GetCompleteQuest(int num) 
    {
        if (num == 1)
            FquestIsDone = true;
        else
            SquestIsDone = true;
    }

    IEnumerator Wait()
    {
        canMove = false;
        yield return new WaitForSeconds(2f);
        canMove = true;
    }

    void MovePlayer(Collider col, GameObject obj)
    {
        var playerPref = col.transform.parent;
        var tempTransform = obj.transform.position;
        playerPref.position = new Vector3(tempTransform.x, tempTransform.y, tempTransform.z);
    }

    private void OnTriggerExit(Collider other)
    {
        //tip.SetActive(false);
        triggerActive = false;
    }

    void setActiveDoor(int num) 
    {
        if (num == 1)
            FquestIsDone = true;
        else
            SquestIsDone= true;
    }

}
