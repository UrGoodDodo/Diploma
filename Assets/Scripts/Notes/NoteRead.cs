using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NoteRead : MonoBehaviour
{

    bool noteChecked = false;

    bool noteOpen = false;

    bool noteRead = false;

    Transform noteInWorld;

    public UnityEngine.UI.Image uiBackground;

    public TextMeshProUGUI uiTextName;

    bool switchedUi = false;

    public ChangeTipStatus tip;

    public static Action NoteIsRead;


    private void OnEnable()
    {
        TagOrLayerChecker.noteChecked += checkNote;
    }

    private void OnDisable()
    {
        TagOrLayerChecker.noteChecked -= checkNote;
    }

    // Start is called before the first frame update
    void Start()
    {
        noteInWorld = GetComponent<Transform>().GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!noteRead)
        {
            if (noteOpen && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)))
            {
                NoteIsRead?.Invoke();
                switchNoteUisState();
                noteOpen = false;
                noteRead = true;
            }

            if (noteChecked && Input.GetKeyDown(KeyCode.E))
            {
                noteOpen = true;
                tip.ChangeTipState(false);
                noteInWorld.gameObject.SetActive(false);
                switchNoteUisState();
                noteChecked = false;
            }
        }

    }

    void checkNote(bool flag) 
    {
        noteChecked = flag;
    }

    void switchNoteUisState() 
    {
        if (!switchedUi)
        {
            uiTextName.alpha = 1f;
            uiBackground.color = new Color(uiBackground.color.r, uiBackground.color.g, uiBackground.color.b, 0.6f);
            switchedUi = true;
        }
        else 
        {
            uiTextName.alpha = 0f;
            uiBackground.color = new Color(uiBackground.color.r, uiBackground.color.g, uiBackground.color.b, 0f);
            switchedUi = false;
        }
    }
}
