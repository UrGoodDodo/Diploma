using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondSceneReturning : MonoBehaviour
{
    static bool keyquestIsDone = false;

    static bool bookquestIsDone = false;
    public GameObject door;

    public delegate void QuestPlay(int number);
    public static event QuestPlay QuestPlayed;

    static bool noteRead = false;
    public GameObject note;

    private void OnEnable()
    {
        NoteRead.NoteIsRead += NoteReadd;
        KeyQuest.KeyQuestSaveComplete += KeyQuestComplete;
        CoreBookQuest.BookQuestSaveComplete += BookQuestComplete;
    }

    private void OnDisable()
    {
        NoteRead.NoteIsRead -= NoteReadd;
        KeyQuest.KeyQuestSaveComplete -= KeyQuestComplete;
        CoreBookQuest.BookQuestSaveComplete -= BookQuestComplete;
    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();

        if (keyquestIsDone)
            QuestPlayed?.Invoke(1);

        if (bookquestIsDone) 
        {
            door.SetActive(true);
            QuestPlayed?.Invoke(2);
        }

        if (noteRead)
            note.SetActive(false);
    }


    void NoteReadd()
    {
        noteRead = true;
    }

    void KeyQuestComplete() 
    {
        keyquestIsDone = true;
    }

    void BookQuestComplete() 
    {
        bookquestIsDone = true;
    }
}
