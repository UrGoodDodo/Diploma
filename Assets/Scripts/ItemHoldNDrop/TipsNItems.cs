using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsNItems : MonoBehaviour
{

    public ChangeTipStatus tip;

    private void OnEnable()
    {
        TagOrLayerChecker.DisableTip += DisAbleTip;
        TagOrLayerChecker.ActivateTip += ActIvateTip;
    }

    private void OnDisable()
    {
        TagOrLayerChecker.DisableTip -= DisAbleTip;
        TagOrLayerChecker.ActivateTip += ActIvateTip;
    }

    void DisAbleTip() 
    {
        tip.ChangeTipState(false);
    }

    void ActIvateTip()
    {
        tip.ChangeTipState(true);
    }
}
