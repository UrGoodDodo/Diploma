using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LoadDialogs : MonoBehaviour
{
    public TextAsset textFile;
    Dictionary<int, List<string>> fileDialogs = new Dictionary<int, List<string>>();

    void Start()
    {
        string readText = textFile.text;
        var areaDialogs = readText.Split('\n');;
        for (int i = 1; i < areaDialogs.Length + 1; i++)
        {
            List<string> mainList = new List<string>();
            var dialogs = areaDialogs[i - 1].Split("$");
            foreach (var dialog in dialogs)
            {
                mainList.Add(dialog);
            }
            fileDialogs.Add(i, mainList);
        }
    }

    public void getCurDialogs(int num, out List<string> d) 
    {
        d = fileDialogs[num];
    }

}
