using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomMovement : MonoBehaviour
{
    public GameObject tip;

    private void OnTriggerEnter(Collider other)
    {
        tip.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            Debug.Log("1");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        tip.SetActive(false);

    }
}
