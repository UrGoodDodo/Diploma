using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkMovement : MonoBehaviour
{
    public ChangeTipStatus tip;

    public bool forwardDirection = true;

    public GameObject positionToMove;

    Collider otherCollider;

    bool triggerActive = false;

    static bool canMove = true;

    bool questIsDone = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            tip.ChangeTipState(true);
            triggerActive = true;
            otherCollider = other;
        }   
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            triggerActive = true;
            otherCollider = other;
        }
    }

    void Update()
    {
        if (triggerActive && canMove) 
        {
            if (Input.GetKeyDown(KeyCode.F)) 
            {
                if (forwardDirection) 
                {
                    MovePlayer(otherCollider, positionToMove);
                    StartCoroutine(Wait());
                }
                else
                {
                    MovePlayer(otherCollider, positionToMove);
                    StartCoroutine(Wait());
                }
            }
        }
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
            tip.ChangeTipState(false);
            triggerActive = false;
            otherCollider = null;
        } 
    }
}
