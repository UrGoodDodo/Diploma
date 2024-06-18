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

    //
    public GameObject obj_for_help;

    //View distance AI
    float view_dist = 100f;
    //
    bool flashlight_on;
    //
    public static bool room_dark = true;

    //
    public static bool flashlight_was_taken = false;

    public static bool helping;

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
                Debug.Log("dark");
                if (!flashlight_on)
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
        if (!helping)
        {
            if (Physics.Raycast(player_camera.transform.position, player_camera.forward, out hit, view_dist))
            {
                flashlight.transform.rotation = Quaternion.RotateTowards(player_camera.rotation, Quaternion.LookRotation(hit.point, Vector3.up), 5 * Time.deltaTime);
            }
        }
        else
        {
            if (obj_for_help != null)
                flashlight.transform.LookAt(obj_for_help.transform);
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


}
