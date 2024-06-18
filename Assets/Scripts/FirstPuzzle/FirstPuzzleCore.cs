using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPuzzleCore : MonoBehaviour
{
    public static bool puzzleComplete = false;

    public List<GameObject> disabledViaQuest = new List<GameObject>();

    int completeTasks = 0;

    public static Action PuzzleIsComplete;

    private void OnEnable()
    {
        DraggingCircles.rotationCheckIsDone += IncreaseCompleteTsks;
        MovingSliders.positionCheckIsDone += IncreaseCompleteTsks;

        foreach (var gameobj in disabledViaQuest)
            gameobj.SetActive(false);
    }

    private void OnDisable()
    {
        DraggingCircles.rotationCheckIsDone -= IncreaseCompleteTsks;
        MovingSliders.positionCheckIsDone -= IncreaseCompleteTsks;
    }

    // Update is called once per frame
    void Update()
    {
        if (completeTasks == 4 && !puzzleComplete) 
        {
            puzzleComplete = true;
            foreach (var gameobj in disabledViaQuest)
                gameobj.SetActive(true);
            var tp = transform.GetComponent<TriggerPuzzle>();
            tp.DisableSelf();
            PuzzleIsComplete?.Invoke();
            OnQuestIsDoneDialog.f_flag_end = true;
        }
    }

    void IncreaseCompleteTsks() 
    {
        completeTasks++;
    }


}
