using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecetAllGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick() 
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0); // при создании главного меню поменять на 1
    }
}
