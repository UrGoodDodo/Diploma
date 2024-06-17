using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneReturning : MonoBehaviour
{
    static bool puzzleIsDone = false;
    public GameObject puzzle;
    public GameObject door;

    static bool noteRead = false;
    public GameObject note;

    private void OnEnable()
    {
        FirstPuzzleCore.PuzzleIsComplete += PuzzleDone;
        NoteRead.NoteIsRead += NoteReadd;
    }

    private void OnDisable()
    {
        FirstPuzzleCore.PuzzleIsComplete -= PuzzleDone;
        NoteRead.NoteIsRead -= NoteReadd;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (puzzleIsDone) 
        {
            puzzle.SetActive(false);
            door.SetActive(true);
        }
           
        if (noteRead)
            note.SetActive(false);
    }

    void PuzzleDone() 
    {
        puzzleIsDone = true;
    }

    void NoteReadd() 
    {
        noteRead = true;
    }

}
