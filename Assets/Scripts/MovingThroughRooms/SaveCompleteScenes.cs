using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveCompleteScenes : MonoBehaviour
{
    private void OnEnable()
    {
        SceneMovement.movedToAnotherScene += SaveCompleteScene;
        SceneMovement.moveTypeEvent += SaveMovementType;
    }


    void SaveCompleteScene()
    {
        int num = SceneManager.GetActiveScene().buildIndex;
        if (PlayerPrefs.HasKey("SavedScenes"))
        {
            string temp = PlayerPrefs.GetString("SavedScenes");
            if (!temp.Contains(num.ToString())) 
            {
                temp += num.ToString();
                PlayerPrefs.SetString("SavedScenes", temp);
                PlayerPrefs.Save();
            }  
        }
        else 
        {
            PlayerPrefs.SetString("SavedScenes", num.ToString());
            PlayerPrefs.Save();
        }
        Debug.Log($"Saved {num}");
    }

    void SaveMovementType(bool flag) 
    {
        PlayerPrefs.SetInt("Direction", Convert.ToInt32(flag));
        Debug.Log($"Saved {flag}  {Convert.ToInt32(flag)}");
        PlayerPrefs.Save();
    }

    private void OnDisable()
    {
        SceneMovement.movedToAnotherScene -= SaveCompleteScene;
        SceneMovement.moveTypeEvent -= SaveMovementType;
    }
}
