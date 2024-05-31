using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadSceneDialog : MonoBehaviour
{
    LinearDialog cd;

    bool activateDialog = false;

    private void Start()
    {
        cd = GetComponent<LinearDialog>();
    }
    void Update()
    {
        if (!activateDialog && cd.GotDialog) 
        {
            cd.clauseFullFilled();
            activateDialog = true;
        }
    }
}
