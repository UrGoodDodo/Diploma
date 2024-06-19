using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthPuzzleCore : MonoBehaviour
{

    public List<int> correctPuzzleAnswer = new List<int>();

    List<int> curPuzzleAnswer = new List<int>();

    public List<GameObject> disabledViaQuest = new List<GameObject>();

    public GameObject Lock;

    public static Action PuzzleIsComplete;

    private void OnEnable()
    {
        InteractiveButtons.ButtonCloncked += AddNewNumber;
        InteractiveButtons.MainButtonClonked += CheckCurAnswer;

        foreach (var gameobj in disabledViaQuest)
            gameobj.SetActive(false);
    }

    private void OnDisable()
    {
        InteractiveButtons.ButtonCloncked -= AddNewNumber;
        InteractiveButtons.MainButtonClonked -= CheckCurAnswer;
    }

    void CheckCurAnswer() 
    {
        bool flag = true;
        if (curPuzzleAnswer.Count == correctPuzzleAnswer.Count) 
        {
            for (int i = 0; i < correctPuzzleAnswer.Count; i++) 
            {
                if(curPuzzleAnswer[i] != correctPuzzleAnswer[i])
                {
                    flag = false;
                    break;
                }
            }
        }

        if (flag) // puzzle complete
        {
            
            foreach (var gameobj in disabledViaQuest)
                gameobj.SetActive(true);

            Lock.gameObject.SetActive(false);

            var tp = transform.GetComponent<TriggerPuzzle>();
            tp.DisableSelf();
            PuzzleIsComplete?.Invoke();
        }
        else
            curPuzzleAnswer.Clear();
    }

    void AddNewNumber(int number) 
    {
        curPuzzleAnswer.Add(number);
    }

}
