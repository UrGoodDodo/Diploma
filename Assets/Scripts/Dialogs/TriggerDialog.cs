using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    public static Action triggerDialog;

    private void OnTriggerEnter(Collider other)
    {
        triggerDialog?.Invoke();
    }
}
