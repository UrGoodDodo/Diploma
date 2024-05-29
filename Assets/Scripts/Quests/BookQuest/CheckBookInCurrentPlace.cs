using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBookInCurrentPlace : MonoBehaviour
{

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private GameObject rightBook;

    [SerializeField]
    private GameObject tipBook;

    Rigidbody currentItemInHands;

    GameObject currentBook;

    private bool playerInArea = false;

    public delegate void SetCurItemNull();
    public static event SetCurItemNull CurItemNullset;

    bool localQuestIsDone = false;

    public delegate void localQuestComplete();
    public static event localQuestComplete localQuestCompleteEvent;

    bool currentBookIsOnPlace = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
        {
            playerInArea = true;
            checkPlayersItem();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
        {
            playerInArea = false;
        }
    }

    void Update()
    {
        if (!localQuestIsDone)
        {
            if (playerInArea) 
            {
                if (currentItemInHands != null) 
                {
                    if (currentItemIsBook()) 
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            Debug.Log("Нажал");
                            placeCurrentBook();
                            if (currentBookIsRight())
                            {
                                currentBook.gameObject.layer = LayerMask.NameToLayer("Default");
                                localQuestIsDone = true;
                                localQuestCompleteEvent?.Invoke();
                            }
                        }
                    }
                    
                }   
            }  
        }

        if (!checkIfCurrentBookIsOnPlace() && currentBookIsOnPlace) 
        {
            switchConditionOfTipBook();
            currentBookIsOnPlace = false;
        }

    }

    void placeCurrentBook() 
    {
        switchConditionOfTipBook();
        currentBook = currentItemInHands.gameObject;
        CurItemNullset?.Invoke();
        moveCurrentBook();
        currentBookIsOnPlace = true;
    }



    bool currentBookIsRight() 
    {
        return rightBook == currentBook;
    }

    bool currentItemIsBook() 
    {
        return currentItemInHands.gameObject.CompareTag("Book");
    }

    void switchConditionOfTipBook() 
    {
        tipBook.SetActive(!tipBook.activeSelf);
    }

    void moveCurrentBook() 
    {
        var tp = tipBook.transform.position;
        var tr = tipBook.transform.rotation;
        currentBook.transform.position = new Vector3(tp.x, tp.y, tp.z);
        currentBook.transform.rotation = tr;
    }

    void checkPlayersItem()
    {
        var temp = cam.GetComponent<HoldNDrop>();
        currentItemInHands = temp.currentlyHoldItem_;
    }

    bool checkIfCurrentBookIsOnPlace() 
    {
        if (currentBook == null)
            return false;
        else
            return (Mathf.Abs(currentBook.transform.position.x - tipBook.transform.position.x) < 0.5) && (Mathf.Abs(currentBook.transform.position.y - tipBook.transform.position.y) < 0.5) && (Mathf.Abs(currentBook.transform.position.z - tipBook.transform.position.z) < 0.5);
    }
}
