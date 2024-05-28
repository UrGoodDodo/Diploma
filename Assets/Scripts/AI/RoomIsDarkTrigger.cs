using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomIsDarkTrigger : MonoBehaviour
{
    public static Action RoomIsDark;

    private void OnTriggerEnter(Collider other)
    {
        RoomIsDark?.Invoke();
    }

}
