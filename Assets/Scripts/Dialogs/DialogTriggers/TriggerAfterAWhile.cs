using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAfterAWhile : MonoBehaviour
{
    LinearDialog cd;

    bool activateDialog = false;

    public float aWhile = 15f;

    private void Start()
    {
        cd = GetComponent<LinearDialog>();
    }
    void Update()
    {
        if (!activateDialog && cd.GotDialog)
        {
            activateDialog = true;
            StartCoroutine(WaitFroAWhile());
        }
    }

    IEnumerator WaitFroAWhile()
    {
        yield return new WaitForSeconds(aWhile);
        cd.clauseFullFilled();
    }

}
