using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void OnEnable()
    {
        OnQuestIsDoneDialog.IsTheEndAction += ExitGame;
    }

    private void OnDisable()
    {
        OnQuestIsDoneDialog.IsTheEndAction -= ExitGame;
    }

    void ExitGame() 
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
