using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnQuestIsDoneDialog : MonoBehaviour
{

    LinearDialog cd;

    bool activateDialog = false;

    bool in_zone;

    public int dialog_num;

    public GameObject active_trigger;

    private void Start()
    {
        if(dialog_num == 3 || dialog_num == 8)
        {
            AIBehavuor.is_helping = true;
        }
        if (dialog_num == 10)
        {
            AIBehavuor.is_start = false;
        }
        cd = GetComponent<LinearDialog>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!activateDialog && cd.GotDialog)
        {
            if(in_zone && Input.GetKeyDown(KeyCode.E))
            {
                LightBehavour.helping = false;
                AIBehavuor.is_helping = false;
                cd.clauseFullFilled();
                activateDialog = true;
                active_trigger.SetActive(true);
            }
            if (dialog_num == 8 && in_zone)
            {
                //тут надо проверить на то, чтобы диалог не включался, если игрок не зайдет в триггер
                LightBehavour.helping = false;
                AIBehavuor.is_helping = false;
                cd.clauseFullFilled();
                activateDialog = true;
            }
            if (dialog_num == 10)
            {
                AIBehavuor.is_start = true;
                cd.clauseFullFilled();
                activateDialog = true;
                AIBehavuor.IsHelping?.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            in_zone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            in_zone = false;
        }
    }
}
