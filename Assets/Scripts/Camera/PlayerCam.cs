using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    bool camIsFixed = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Курсос залочен в центре экрана
        Cursor.visible = false; // Видимость курсора
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!camIsFixed) 
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Не смотрим вверх или вниз больше чем на 90 градусов

            // Ротейтим камеру и ориентэйшион
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        } 
    }


    void ChangeCamStatus() 
    {
        camIsFixed = !camIsFixed;
    }

    private void OnEnable()
    {
        TriggerPuzzle.changeCamState += ChangeCamStatus;
    }

    private void OnDisable()
    {
        TriggerPuzzle.changeCamState -= ChangeCamStatus;
    }
}
