using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReset : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) 
        {
            PlayerPrefs.DeleteAll();
            Application.Quit();
        }
    }
}
