using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturningToOldScenes : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    public Transform endDestination;

    public static Action OnLoadHideDialogs;

    bool direction;

    bool isDone = false;

    private void Start()
    {
        if (PlayerPrefs.HasKey("SavedScenes"))
        {
            string temp = PlayerPrefs.GetString("SavedScenes");

            if (temp.Contains((SceneManager.GetActiveScene().buildIndex).ToString()))
            {
                OnLoadHideDialogs?.Invoke();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDone)
        {
            if (PlayerPrefs.HasKey("Direction"))
            {
                int temp = PlayerPrefs.GetInt("Direction");
                direction = Convert.ToBoolean(temp);
            }
            else
            {
                Debug.Log("Nothing was saved");
            }

            if (PlayerPrefs.HasKey("SavedScenes"))
            {
                string temp = PlayerPrefs.GetString("SavedScenes");

                if (temp.Contains((SceneManager.GetActiveScene().buildIndex).ToString())) 
                {
                    OnLoadHideDialogs?.Invoke();
                    if (!direction) 
                    {
                        player.transform.position = new Vector3(endDestination.position.x, endDestination.position.y, endDestination.position.z);
                    }
                }
            }
            else
            {
                Debug.Log("Nothing was saved");
            }
            isDone = true;
        }
        
    }

}
