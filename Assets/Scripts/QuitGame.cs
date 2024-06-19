using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void OnEnable()
    {
        DialogCore.IsTheEndAction += ExitGame;
    }

    private void OnDisable()
    {
        DialogCore.IsTheEndAction -= ExitGame;
    }

    void ExitGame() 
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
