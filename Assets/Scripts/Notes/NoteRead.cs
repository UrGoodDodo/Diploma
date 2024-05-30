using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NoteRead : MonoBehaviour
{

    bool noteChecked = false;

    bool noteOpen = false;

    bool noteRead = false;

    Transform noteInWorld;

    public GameObject noteBackground;

    public GameObject noteText;



    private void OnEnable()
    {
        HoldNDrop.noteChecked += checkNote;
    }

    private void OnDisable()
    {
        HoldNDrop.noteChecked -= checkNote;
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
            if (noteOpen && Input.GetKeyDown(KeyCode.Escape))
            {
                switchNoteUisState();
                noteOpen = false;
                noteRead = true;
            }

            if (noteChecked && Input.GetKeyDown(KeyCode.E))
            {
                noteOpen = true;
                noteInWorld.gameObject.SetActive(false);
                switchNoteUisState();
            }
        }

    }

    void checkNote(bool flag) 
    {
        noteChecked = flag;
    }

    void switchNoteUisState() 
    {
        noteBackground.SetActive(!noteBackground.activeSelf);
        noteText.SetActive(!noteText.activeSelf);
    }
}
