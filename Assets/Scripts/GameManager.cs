using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        LightBehavour.FlashlightExist += Exist;
    }

    private void OnDisable()
    {
        LightBehavour.FlashlightExist -= Exist;
    }

    public void Exist()
    {
        Debug.Log("flash");
        if(!LightBehavour.flashlight_exist)
            LightBehavour.flashlight_exist = true;
        
    }
}
