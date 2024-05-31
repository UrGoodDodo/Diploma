using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        RoomIsDarkTrigger.RoomIsDark += RoomDark;
        TriggerR1Behavour.StartedKeyTrigger += SearchingKey;
        TriggerR2Behavour.StartBookTrigger += BooksPuzzle;
        KeyQuest.KeyIsFound += KeyFound;
        FlashlightChip.FlashlightTaken += Taken;
        DogMovement.ChangeRoom += InNewRoom;
        AIBehavuor.IsHelping += IsNeedHelp;
    }

    private void OnDisable()
    {
        RoomIsDarkTrigger.RoomIsDark -= RoomDark;
        TriggerR1Behavour.StartedKeyTrigger -= SearchingKey;
        TriggerR2Behavour.StartBookTrigger -= BooksPuzzle;
        KeyQuest.KeyIsFound -= KeyFound;
        FlashlightChip.FlashlightTaken -= Taken;
        DogMovement.ChangeRoom -= InNewRoom;
        AIBehavuor.IsHelping -= IsNeedHelp;
    }

    public void Taken()
    {
        //переменную нужно мен€ть с учетом, что фонарик подобран, поэтому в скрипте плейера надо создать событие
        if(!LightBehavour.flashlight_was_taken)
            LightBehavour.flashlight_was_taken = true;
        //здесь надо написать код, мен€ющий в скрипте лайтинга значение переменной flashlight_was_taken на тру 
    }

    public void SearchingKey()
    {
        TriggerR1Behavour.IsTriggeredR1 = true;
        AIBehavuor.is_searching_key = true;
        AIBehavuor.search_number = 0;
    }

    public void BooksPuzzle()
    {

    }

    public void RoomDark()
    {
        LightBehavour.room_dark = false;
    }

    public void KeyFound()
    {
        if(!AIBehavuor.key_was_found)
            AIBehavuor.key_was_found = true;
    }

    public void InNewRoom()
    {
        DogMovement.current_room = RoomMovement.dog_room;
    }

    public void IsNeedHelp()
    {
        if (!LightBehavour.helping)
            LightBehavour.helping = true;
    }

}
