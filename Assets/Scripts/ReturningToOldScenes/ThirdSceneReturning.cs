using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdSceneReturning : MonoBehaviour
{
    static bool noteRead = false;
    public GameObject note;

    private void OnEnable()
    {
        NoteRead.NoteIsRead += NoteReadd;
    }

    private void OnDisable()
    {
        NoteRead.NoteIsRead -= NoteReadd;
    }


    void Start()
    {
        if (noteRead)
            note.SetActive(false);
    }

    void NoteReadd()
    {
        noteRead = true;
    }
}
