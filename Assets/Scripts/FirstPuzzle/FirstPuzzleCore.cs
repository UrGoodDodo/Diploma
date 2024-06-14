using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPuzzleCore : MonoBehaviour
{

    GameObject puzzleGameObject;

    bool puzzleComplete = false;

    

    // Start is called before the first frame update
    void Start()
    {
        puzzleGameObject = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
