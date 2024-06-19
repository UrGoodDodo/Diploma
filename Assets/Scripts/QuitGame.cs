using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    void ExitGame() 
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
