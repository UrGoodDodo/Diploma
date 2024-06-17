using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DialogCore : MonoBehaviour
{
    int sceneNum;

    public static bool dialogsAreActive = false;

    List<string> sceneDialogs;
    bool gotDialogs = false;

    public List<GameObject> dialogTiggers = new List<GameObject>();

    int countPlayedDialogs = 0;

    public int CountPlayedDialogs
    {
        get { return countPlayedDialogs+1; }
    }

    bool currentDialogType = false; // false - non linear ; true - linear

    public bool GotDialogs_ 
    {
        get { return gotDialogs; }
    }

    public delegate void DialogsPlay(int number);
    public static event DialogsPlay DialogsArePlayed;

    private void OnEnable()
    {
        LinearDialog.switchCoreDialogStatus += changeDialogStatus;
        LinearDialog.startedForwardDialog += SwitchDialogType;
        NonLinearDialog.switchCoreDialogStatus += changeDialogStatus;
        NonLinearDialog.startedMiscDialog += SwitchDialogType;

        ReturningToOldScenes.OnLoadHideDialogs += DisableAllDialogTriggers;
    }

    private void OnDisable()
    {
        LinearDialog.switchCoreDialogStatus -= changeDialogStatus;
        LinearDialog.startedForwardDialog -= SwitchDialogType;
        NonLinearDialog.switchCoreDialogStatus -= changeDialogStatus;
        NonLinearDialog.startedMiscDialog -= SwitchDialogType;

        ReturningToOldScenes.OnLoadHideDialogs -= DisableAllDialogTriggers;

    }

    void Start()
    {
        sceneNum = SceneManager.GetActiveScene().buildIndex + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gotDialogs)
        {
            getCurrentDialogs(sceneNum);
        }
    }

    void changeDialogStatus() 
    {
        dialogsAreActive = !dialogsAreActive;
        if (!dialogsAreActive && currentDialogType)
        {
            dialogTiggers[countPlayedDialogs].SetActive(false);
            countPlayedDialogs++;
            if (countPlayedDialogs <= dialogTiggers.Count - 1) 
            {
                dialogTiggers[countPlayedDialogs].SetActive(true);

                if (countPlayedDialogs == dialogTiggers.Count - 1) 
                    DialogsArePlayed?.Invoke(sceneNum);
                    
            }
        }
    }

    void getCurrentDialogs(int sceneNum)
    {
        var ld = GetComponent<LoadDialogs>();
        ld.getCurDialogs(sceneNum, out sceneDialogs);
        gotDialogs = true;
    }

    public void getCurrentDialog(int numDialog, out string dialog) 
    {
        dialog = sceneDialogs.Count >= numDialog ? sceneDialogs[numDialog-1] : 0.ToString();
    }

    void SwitchDialogType(bool typeOfDialog) 
    {
        currentDialogType = typeOfDialog;
    }

    void DisableAllDialogTriggers() 
    {
        foreach (var item in dialogTiggers)
        {
            item.SetActive(false);
        }
    }
}
