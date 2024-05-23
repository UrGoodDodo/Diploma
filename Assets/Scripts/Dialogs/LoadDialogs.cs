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
    Dictionary<int, Queue<string>> maincharacterDialogs = new Dictionary<int, Queue<string>>();
    Dictionary<int, Queue<string>> aiDialogs = new Dictionary<int, Queue<string>>();
    void Start()
    {
        string readText = textFile.text;
        var areaDialogs = readText.Split('\n');;
        for (int i = 1; i < areaDialogs.Length + 1; i++)
        {
            Queue<string> mainQueue = new Queue<string>();
            Queue<string> aiQueue = new Queue<string>();
            var dialogs = areaDialogs[i - 1].Split("$");
            foreach (var dialog in dialogs)
            {
                var sentences = dialog.Split(";");
                foreach (var sentence in sentences)
                {
                    int count = sentences.Count() - 1;
                    var supSentence = sentence.Split(">");
                    if (supSentence[1].Equals("Ã") || supSentence[1].Length == 2)
                    {
                        mainQueue.Enqueue(supSentence[0]);

                    }
                    else 
                    {
                        aiQueue.Enqueue(supSentence[0]);
                    }

                }
            }
            maincharacterDialogs.Add(i, mainQueue);
            aiDialogs.Add(i, aiQueue);
        }

        //for (int i = 1; i <= 5; i++)
        //{
        //    var t = maincharacterDialogs[i].Count;
        //    for (int j = 0; j < t; j++)
        //    {
        //        var tt = maincharacterDialogs[i].Dequeue();
        //        Debug.Log($" {i}  {tt}");
        //    }
        //}
    }

}
