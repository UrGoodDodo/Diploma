using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveButtons : MonoBehaviour
{

    public delegate void ButtonClonck(int number);
    public static event ButtonClonck ButtonCloncked;

    public static Action MainButtonClonked;

    public void ButtonClick(int i) 
    {
        ButtonCloncked?.Invoke(i);
    }

    public void MainButtonClick() 
    {
        MainButtonClonked?.Invoke();
    }
}
