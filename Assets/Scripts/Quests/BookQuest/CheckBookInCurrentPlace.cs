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

    private void OnTriggerEnter(Collider other)
    {
        playerInArea = true;
        checkPlayersItem();
    }

    private void OnTriggerExit(Collider other)
    {
        playerInArea = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!localQuestIsDone) 
        {
            if (playerInArea)
            {
                if (currentItemInHands != null)
                {
                    if (currentItemIsBook())
                    {
                        if (Input.GetKey(KeyCode.F))
                        {
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
        
    }

    void placeCurrentBook() 
    {
        switchConditionOfTipBook();
        currentBook = currentItemInHands.gameObject;
        CurItemNullset?.Invoke();
        moveCurrentBook();
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
        var tt = tipBook.transform.position;
        var tr = tipBook.transform.rotation;
        currentBook.transform.position = new Vector3(tt.x, tt.y, tt.z);
        currentBook.transform.rotation = tr;
    }

    void checkPlayersItem()
    {
        var temp = cam.GetComponent<HoldNDrop>();
        currentItemInHands = temp.currentlyHoldItem_;
    }
}
