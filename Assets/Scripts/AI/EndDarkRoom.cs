using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDarkRoom : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        LightBehavour.room_dark = true;
    }
}
