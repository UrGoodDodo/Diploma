using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonPressTrigger : MonoBehaviour
{

    private void Start()
    {
        cd = GetComponent<LinearDialog>();
    }

    LinearDialog cd;

    bool activateDialog = false;

    bool playerInArea = false;

    public List<GameObject> disableViaNoTrigger = new List<GameObject>();

    public ChangeTipStatus tip;

    private void OnEnable()
    {
        foreach (var item in disableViaNoTrigger)
        {
            item.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = true;
            tip.ChangeTipState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = false;
            tip.ChangeTipState(false);
        }
    }

    private void Update()
    {
        if (!activateDialog)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (cd != null)
                {

                    if (!DialogCore.dialogsAreActive && playerInArea)
                    {
                        cd.clauseFullFilled();
                        activateDialog = true;
                        tip.ChangeTipState(false);
                        foreach (var item in disableViaNoTrigger)
                        {
                            item.SetActive(true);
                        }
                    }
                }
            }

        }
    }
}
