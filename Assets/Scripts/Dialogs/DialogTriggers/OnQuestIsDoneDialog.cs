using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnQuestIsDoneDialog : MonoBehaviour
{

    LinearDialog cd;

    bool activateDialog = false;

    bool in_zone;

    public static bool f_flag_end = false;

    public int dialog_num;

    public GameObject active_trigger;

    public static bool the_end_dialoge = false;

    private void Start()
    {
        if(dialog_num == 3 || dialog_num == 8 || dialog_num == 9)
        {
            AIBehavuor.is_helping = true;
        }
        cd = GetComponent<LinearDialog>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!activateDialog && cd.GotDialog)
        {
            if(dialog_num == 3 && f_flag_end)
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
            if (dialog_num == 9 && active_trigger)
            {
                AIBehavuor.is_helping = false;
            }
            if (dialog_num == 10)
            {
                cd.clauseFullFilled();
                activateDialog = true;
            }
            if (dialog_num == 11 && the_end_dialoge)
            {
                cd.clauseFullFilled();
                activateDialog = true;
                active_trigger.SetActive(true);
            }
            if (dialog_num == 12)
            {
                cd.clauseFullFilled();
                activateDialog = true;
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
