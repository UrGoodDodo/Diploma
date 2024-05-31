using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnQuestIsDoneDialog : MonoBehaviour
{

    LinearDialog cd;

    bool activateDialog = false;

    private void Start()
    {
        cd = GetComponent<LinearDialog>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!activateDialog && cd.GotDialog)
        {
            cd.clauseFullFilled();
            activateDialog = true;
        }
    }
}
