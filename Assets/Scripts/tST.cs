using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tST : MonoBehaviour
{

    Transform tr;

    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            Debug.Log("adads");
            StartCoroutine(tpObject());
            
        }
    }

    IEnumerator tpObject() 
    {
        for (int i = 0; i < 4; i++)
        {
            
            tr.position = new Vector3(tr.position.x + 5, tr.position.y, tr.position.z);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
