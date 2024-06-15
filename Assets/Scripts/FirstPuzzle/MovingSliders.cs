using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovingSliders : MonoBehaviour, IPointerUpHandler
{

    bool rightPlace = false;

    Slider slider;

    float curPosition = 0;

    public float rightPosition = 0.3f; // from 0 to 1

    public static float accuracy = 0.08f;

    public static Action positionCheckIsDone;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!rightPlace)
        {
            if (Math.Abs(curPosition - rightPosition) <= accuracy) 
            {
                rightPlace = true;
                slider.interactable = false;
                slider.value = rightPosition;
                positionCheckIsDone?.Invoke();
            }
        }
        
    }

    public void OnValueChanged(float newValue)
    {
        curPosition = newValue;
    }

}
