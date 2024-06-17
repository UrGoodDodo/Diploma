using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthSceneReturning : MonoBehaviour
{
    static bool puzzleIsDone = false;
    public GameObject puzzle;
    public GameObject door;


    private void OnEnable()
    {
        FourthPuzzleCore.PuzzleIsComplete += PuzzleDone;
    }

    private void OnDisable()
    {
        FourthPuzzleCore.PuzzleIsComplete -= PuzzleDone;
    }

    void Start()
    {
        if (puzzleIsDone)
        {
            puzzle.SetActive(false);
            door.SetActive(true);
        }
    }

    void PuzzleDone()
    {
        puzzleIsDone = true;
    }

}
