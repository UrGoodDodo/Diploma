using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class LightBehavour : MonoBehaviour
{
    //
    public Transform player_camera;
    //Position of AI
    public Transform ai_position;
    //
    public Light flashlight;
    //View distance AI
    float view_dist = 100f;
    //
    bool flashlight_on;
    //
    bool flashlight_off;
    //
    public static bool room_dark;

    //
    public static bool flashlight_was_taken = false;
    //
    public static bool flashlight_exist = false;
    //
    public static Action FlashlightExist;

    // Start is called before the first frame update
    void Start()
    {
        TurnOff();
    }

    // Update is called once per frame
    void Update()
    {
        RotateFlashlight();
        if (flashlight_was_taken)
        {
            if (room_dark)
            {
                if(!flashlight_on)
                    TurnOn();
            }
            else if(flashlight_on) 
                TurnOff();
        }
    }

    //The flashlight looks towards the direction of the players camera
    private void RotateFlashlight()
    {
        RaycastHit hit;
        if (Physics.Raycast(player_camera.transform.position, player_camera.forward, out hit, view_dist))
        {
            flashlight.transform.rotation = Quaternion.RotateTowards(player_camera.rotation, Quaternion.LookRotation(hit.point, Vector3.up), 5 * Time.deltaTime);
        }
    }

    //
    private void TurnOn()
    {
        if (flashlight.enabled == false)
        {
            flashlight.enabled = true;
            flashlight_on = true;
        }
    }

    //
    private void TurnOff()
    {
        if (flashlight.enabled == true)
        {
            flashlight.enabled = false;
            flashlight_on = false;
        }
    }

    //
    private void OnCollisionEnter(Collision collision)
    {
        if(flashlight_was_taken)
            if(!flashlight_exist)
                FlashlightExist?.Invoke();
    }


}
