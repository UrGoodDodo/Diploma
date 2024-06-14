using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangeTipStatus : MonoBehaviour
{

    UnityEngine.UI.Image tipBack;

    TextMeshProUGUI tipText;

    private void Start()
    {
        tipBack = transform.gameObject.GetComponent<UnityEngine.UI.Image>();
        tipText = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        tipText.alpha = 0f;
        tipBack.color = new Color(tipBack.color.r, tipBack.color.g, tipBack.color.b, 0.0f);
    }

    public void ChangeTipState(bool state) 
    {
        if (state)
        {
            tipText.alpha = 1f;
            tipBack.color = new Color(tipBack.color.r, tipBack.color.g, tipBack.color.b, 0.5f);
        }
        else 
        {
            tipText.alpha = 0f;
            tipBack.color = new Color(tipBack.color.r, tipBack.color.g, tipBack.color.b, 0.0f);
        }
    }
}
