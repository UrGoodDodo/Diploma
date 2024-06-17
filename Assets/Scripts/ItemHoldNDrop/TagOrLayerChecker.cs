using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TagOrLayerChecker : MonoBehaviour
{

    GameObject oldGameObject;

    public delegate void OutLineIt(GameObject rigidbody);
    public static OutLineIt OutLinedIt;

    public static Action DisableOldOutline;
    public static Action ActivateTip;
    public static Action DisableTip;
    bool disableOnce = false;

    public delegate void Note(bool flag);
    public static event Note noteChecked;

    //public delegate void PuzzleItem(int num, bool flag);
    //public static event PuzzleItem puzzledItemd;

    private void OnEnable()
    {
        HoldNDrop.CheckedCurRigidBody += CheckCurRigidBody;
    }

    private void OnDisable()
    {
        HoldNDrop.CheckedCurRigidBody -= CheckCurRigidBody;
    }

    void CheckCurRigidBody(GameObject gameObject) 
    {
        if (gameObject != null) 
        {
            if (gameObject.layer == LayerMask.NameToLayer("InteractableItems"))
            {
                OutLinedIt?.Invoke(gameObject);

                if (gameObject.CompareTag("ItemsWithTips"))
                {
                    ActivateTip?.Invoke();
                    disableOnce = true;

                    if (gameObject.transform.childCount > 0)
                    {
                        var tagCheck = gameObject.transform.GetChild(0);
                        if (tagCheck.CompareTag("Note"))
                            noteChecked?.Invoke(true);
                        else
                            noteChecked?.Invoke(false);
                    }

                    //if (tagCheck.CompareTag("PuzzleItem"))
                    //{
                    //    var t = gameObject.GetComponent<PuzzleItems>().plateNumber;
                    //    puzzledItemd?.Invoke(t, true);
                    //}
                    //else 
                    //    puzzledItemd?.Invoke(0, false);

                }

                oldGameObject = gameObject;
            }
            else if (oldGameObject != null)
            {
                DisableOldOutline?.Invoke();

                if (oldGameObject.CompareTag("ItemsWithTips") && disableOnce)
                {
                    DisableTip?.Invoke();
                    disableOnce = false;
                }

                if (oldGameObject.transform.childCount > 0)
                {
                    if (oldGameObject.transform.GetChild(0).CompareTag("Note"))
                        noteChecked?.Invoke(false);

                    //if (oldGameObject.transform.GetChild(0).CompareTag("PuzzleItem"))
                    //    puzzledItemd?.Invoke(0, false);
                }
            }
        }
        else
            DisableOldOutline?.Invoke();


    }

}
