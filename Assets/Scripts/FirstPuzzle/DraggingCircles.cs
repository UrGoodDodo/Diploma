using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggingCircles : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    RectTransform m_RectTransform;

    bool rightPlace = false;

    public float rightRotation = 144f; // from -180 to 180

    public static float accuracy = 8f;

    public static Action rotationCheckIsDone; 

    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!rightPlace) 
        {
            var direction = Input.mousePosition - m_RectTransform.position;
            m_RectTransform.up = direction;
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!rightPlace)
        {
            var direction = Input.mousePosition - m_RectTransform.position;
            m_RectTransform.up = direction;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!rightPlace) 
        {
            var tempRot = m_RectTransform.transform.eulerAngles.z > 180 ?  m_RectTransform.transform.eulerAngles.z - 360 : m_RectTransform.transform.eulerAngles.z;
            print(tempRot);
            print(rightRotation);
            print(Math.Abs(tempRot - rightRotation));
            if (Math.Abs(tempRot - rightRotation) <= accuracy)
            {
                var tempRectTransform = m_RectTransform.transform.eulerAngles;
                tempRectTransform.z = rightRotation;
                m_RectTransform.transform.eulerAngles = tempRectTransform;
                rotationCheckIsDone?.Invoke();
                rightPlace = true;
            }
        }
    }
}
