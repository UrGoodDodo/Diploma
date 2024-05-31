using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NonLinearDialog : MonoBehaviour
{

    bool condition = false;

    public bool repetable = false;

    bool dialogPlayed = false;

    bool switchedUi = false;

    public int dialogDependent = 0;

    public DialogCore dialogCore;

    public UnityEngine.UI.Image uiBackground;

    public TextMeshProUGUI uiTextName;

    public TextMeshProUGUI uiText;

    public delegate void startMiscDialog(bool dialType);
    public static startMiscDialog startedMiscDialog;

    public static Action switchCoreDialogStatus;

    public string Name = "";

    public string Text = "";

    public List<GameObject> disabledWiaDialog = new List<GameObject>();


    // Update is called once per frame
    void Update()
    {   if (dialogDependent == 0 || dialogCore.CountPlayedDialogs < dialogDependent) 
        {
            if (condition && DialogCore.dialogsAreActive)
            {
                condition = false;
            }
            if (repetable) // ограничение на само срабатывание в триггере (частота)
            {
                if (condition)
                {
                    StartCoroutine(changeTextnName());
                }
            }
            else
            {
                if (!dialogPlayed)
                {
                    if (condition)
                    {
                        dialogPlayed = true;
                        StartCoroutine(changeTextnName());
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    IEnumerator changeTextnName()
    {
        switchCoreDialogStatus?.Invoke();
        startedMiscDialog?.Invoke(false);
        changeDialogWindowStatus();
        SwitchStatusOnObjectsDuringDialog();

        var time = Text.Length * 0.05f + 2f;
        StartCoroutine(PrintDialogText(Name, Text, 0.05f));
        yield return new WaitForSeconds(time);

        changeDialogWindowStatus();
        SwitchStatusOnObjectsDuringDialog();
        switchCoreDialogStatus?.Invoke();
    }

    IEnumerator PrintDialogText(string name, string text, float delay)
    {
        var tn = uiTextName.GetComponent<TextMeshProUGUI>();
        var ts = "";
        foreach (var t in name)
        {
            ts += t;
            tn.text = ts;
            yield return null;
        }
        
        var tt = uiText.GetComponent<TextMeshProUGUI>();
        var tts = "";
        foreach (var sym in text)
        {
            tts += sym;
            tt.text = tts;
            yield return new WaitForSeconds(delay);
        }
    }

    void changeDialogWindowStatus()
    {
        if (!switchedUi)
        {
            uiTextName.alpha = 1f;
            uiText.alpha = 1f;
            uiBackground.color = new Color(uiBackground.color.r, uiBackground.color.g, uiBackground.color.b, 0.5f);
            switchedUi = true;
        }
        else
        {
            uiTextName.alpha = 0f;
            uiTextName.text = "";
            uiText.alpha = 0f;
            uiText.text = "";
            uiBackground.color = new Color(uiBackground.color.r, uiBackground.color.g, uiBackground.color.b, 0f);
            switchedUi = false;
        }
    }

    void SwitchStatusOnObjectsDuringDialog()
    {
        foreach (var obj in disabledWiaDialog)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }

    public void clauseFullFilled()
    {
        condition = true;
    }


}
