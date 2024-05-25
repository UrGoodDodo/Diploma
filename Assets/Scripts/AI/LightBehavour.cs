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
    bool room_dark;


    //
    public static bool flashlight_exist = false;
    //
    public static Action FlashlightExist;

    // Start is called before the first frame update
    void Start()
    {
        TurnOff();
    }

    private void OnTriggerEnter(Collider other)
    {
        room_dark = true;
    }

    // Update is called once per frame
    void Update()
    {
        RotateFlashlight();
        if (flashlight_exist)
        {
            OnTriggerEnter(ai_position.GetComponent<Collider>());
            if (room_dark)
            {
                TurnOn();
            }
            else TurnOff();
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
            flashlight.enabled = true;
    }

    //
    private void TurnOff()
    {
        if (flashlight.enabled == true)
            flashlight.enabled = false;
    }

    //
    private void OnCollisionEnter(Collision collision)
    {
        FlashlightExist?.Invoke();
    }
}
