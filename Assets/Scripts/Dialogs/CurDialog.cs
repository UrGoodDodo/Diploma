using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CurDialog : MonoBehaviour
{
    public int dialogNumber;

    bool dialogPlayed = false;

    bool condition = false;

    bool gotDialog = false;

    public static Action startedDialog;

    public DialogCore DialogCore;

    string sentencesString;

    List<Tuple<string, int>> dialogSentences = new List<Tuple<string, int>>();

    public UnityEngine.UI.Image uiBackground;

    public TextMeshProUGUI uiTextName;

    public TextMeshProUGUI uiText;

    bool switchedUi = false;


    private void OnEnable()
    {
        TriggerDialog.triggerDialog += clauseFullFilled;
    }

    private void OnDisable()
    {
        TriggerDialog.triggerDialog -= clauseFullFilled;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gotDialog && DialogCore.GotDialogs_)
        {
            getCurrentDialog();
        }
        if (condition && DialogCore.dialogsAreActive)
        {
            condition = false;
        }
        if (!dialogPlayed)
        {
            if (condition)
            {
                startedDialog?.Invoke();
                changeDialogWindowStatus();

                StartCoroutine(changeTextnName());
                dialogPlayed = true;
            }
        }
    }

    void getCurrentDialog() 
    {
        DialogCore.getCurrentDialog(dialogNumber, out sentencesString);
        parseSentences();
        gotDialog = true;
    }



    void parseSentences() 
    {
        var tsentences = sentencesString.Split(';');
        foreach (var tsentence in tsentences) 
        {
            var tsupsentences = tsentence.Split(">");
            if (tsupsentences[1].Equals("Г") || tsupsentences[1].Length == 2)
                dialogSentences.Add(new Tuple<string, int>(tsupsentences[0], 1));
            else
                dialogSentences.Add(new Tuple<string, int>(tsupsentences[0], 2));
        }
    }

    void clauseFullFilled() 
    {
        condition = true;
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
            uiText.alpha = 0f;
            uiBackground.color = new Color(uiBackground.color.r, uiBackground.color.g, uiBackground.color.b, 0f);
            switchedUi = false;
        }
    }

    IEnumerator changeTextnName() 
    {
        foreach (var sentence in dialogSentences) 
        {
            StartCoroutine(PrintDialogText(sentence, 0.1f));
            yield return new WaitForSeconds(8f);
        }
        changeDialogWindowStatus();
    }

    IEnumerator PrintDialogText(Tuple<string, int> str, float delay)
    {
        var tn = uiTextName.GetComponent<TextMeshProUGUI>();
        if (str.Item2 == 1)
        {
            var s = "Герой";
            var ts = "";
            foreach (var t in s)
            {
                ts += t;
                tn.text = ts;
                yield return null;
            }
        }
        else 
        {
            var s = "Doggy doggy";
            var ts = "";
            foreach (var t in s)
            {
                ts += t;
                tn.text = ts;
                yield return null;
            }
        }

        var tt = uiText.GetComponent<TextMeshProUGUI>();
        var tts = "";
        foreach (var sym in str.Item1)
        {
            tts += sym;
            tt.text = tts;
            yield return new WaitForSeconds(delay);
        }
    }




}
