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

    public bool GotDialogs_ 
    {
        get { return gotDialogs; }
    }


    private void OnEnable()
    {
        CurDialog.startedDialog += changeDialogStatus;
    }

    private void OnDisable()
    {
        CurDialog.startedDialog -= changeDialogStatus;
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
}
