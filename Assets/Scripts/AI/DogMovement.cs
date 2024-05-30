using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    public static int current_room;

    public GameObject Dog1;
    public GameObject Dog2;
    public GameObject Dog3;

    public static Action ChangeRoom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeRoom?.Invoke();
        if(current_room == 0 && (!Dog1.active))
        {
            Dog1.SetActive(true);
            Dog2.SetActive(false);
            Dog3.SetActive(false);
        }
        else if(current_room == 1 && (!Dog2.active))
        {
            Dog1.SetActive(false);
            Dog2.SetActive(true);
            Dog3.SetActive(false);
        }
        else if(current_room == 2 && (!Dog3.active))
        {
            Dog1.SetActive(false);
            Dog2.SetActive(false);
            Dog3.SetActive(true);
        }
    }
}
