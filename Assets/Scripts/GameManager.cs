using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        RoomIsDarkTrigger.RoomIsDark += RoomDark;
        LightBehavour.FlashlightExist += Exist;
        TriggerR1Behavour.StartedKeyTrigger += SearchingKey;
        TriggerR2Behavour.StartBookTrigger += BooksPuzzle;
    }

    private void OnDisable()
    {
        RoomIsDarkTrigger.RoomIsDark -= RoomDark;
        LightBehavour.FlashlightExist -= Exist;
        TriggerR1Behavour.StartedKeyTrigger -= SearchingKey;
        TriggerR2Behavour.StartBookTrigger -= BooksPuzzle;
    }

    public void Exist()
    {
        if(!LightBehavour.flashlight_exist)
            LightBehavour.flashlight_exist = true;
        
    }

    public void Taken()
    {
        //���������� ����� ������ � ������, ��� ������� ��������, ������� � ������� ������� ���� ������� �������
        if(!LightBehavour.flashlight_was_taken)
            LightBehavour.flashlight_was_taken = true;
        //����� ���� �������� ���, �������� � ������� �������� �������� ���������� flashlight_was_taken �� ��� 
    }

    public void SearchingKey()
    {
        TriggerR1Behavour.IsTriggeredR1 = true;
        AIBehavuor.is_searching_key = true;
    }

    public void BooksPuzzle()
    {

    }

    public void RoomDark()
    {
        LightBehavour.room_dark = true;
    }
}
