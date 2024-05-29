using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoreBookQuest : MonoBehaviour
{
    private int RightBooksOnPlaceCount = 0;

    private bool questIsSolved = false;

    public delegate void questComplete(int num);
    public static event questComplete questCompleteEvent;

    private void OnEnable()
    {
        CheckBookInCurrentPlace.localQuestCompleteEvent += increaseRightBooksOnPlaceCount;
    }

    private void OnDisable()
    {
        CheckBookInCurrentPlace.localQuestCompleteEvent -= increaseRightBooksOnPlaceCount;
    }

    void Update()
    {
        if (RightBooksOnPlaceCount == 4)
        {
            questIsSolved = true;
            questCompleteEvent?.Invoke(2);
        }
    }

    void increaseRightBooksOnPlaceCount() 
    {
        RightBooksOnPlaceCount++;
    }
}
