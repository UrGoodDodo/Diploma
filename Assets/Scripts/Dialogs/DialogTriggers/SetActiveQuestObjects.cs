using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveQuestObjects : MonoBehaviour
{

    public List<GameObject> gameObjects = new List<GameObject>();

    public void ActivateObjects() 
    {
        foreach (GameObject obj in gameObjects) 
        {
            obj.SetActive(true);
        }
    }

}
