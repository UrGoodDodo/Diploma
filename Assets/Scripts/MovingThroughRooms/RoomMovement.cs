using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class RoomMovement : MonoBehaviour
{
    public bool forwardDirection = true;

    public int curRoom = 1;

    public GameObject positionToMove;

    Collider otherCollider;

    bool inTriggerZone = false;

    static bool canMove = true;

    bool FquestIsDone = false;

    bool SquestIsDone = false;

    public static int dog_room;

    public ChangeTipStatus tip;


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
        if (other.gameObject.CompareTag("Player"))
        {
            inTriggerZone = true;
            otherCollider = other;
            if (forwardDirection)
            {
                if (curRoom == 1 && FquestIsDone)
                    tip.ChangeTipState(true);
                if (curRoom == 2 && SquestIsDone)
                    tip.ChangeTipState(true);
            }
            else 
            {
                tip.ChangeTipState(true);
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTriggerZone = true;
            if (curRoom == 1 && FquestIsDone)
                tip.ChangeTipState(true);
            if (curRoom == 2 && SquestIsDone)
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
                {
                    if (curRoom == 1 && FquestIsDone)
                    {
                        dog_room = curRoom;
                        MovePlayer(otherCollider, positionToMove);
                        tip.ChangeTipState(false);
                        StartCoroutine(Wait());
                    }
                    if (curRoom == 2 && SquestIsDone)
                    {
                        dog_room = curRoom;
                        MovePlayer(otherCollider, positionToMove);
                        tip.ChangeTipState(false);
                        StartCoroutine(Wait());
                    }
                }
                else
                {
                    dog_room = curRoom;
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
        if (other.gameObject.CompareTag("Player"))
        {
            inTriggerZone = false;
            otherCollider = null;
            tip.ChangeTipState(false);
        }
    }

    void setActiveDoor(int num) 
    {
        if (AIBehavuor.is_searching_key)
            AIBehavuor.is_searching_key = false;

        if (num == 1)
            FquestIsDone = true;
        else
            SquestIsDone= true;
    }

}
