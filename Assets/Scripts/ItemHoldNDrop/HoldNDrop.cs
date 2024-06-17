using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoldNDrop : MonoBehaviour
{

    float offset = 1f;

    private bool isDragging = false;
    private Rigidbody currentlyHoldItem;

    public Rigidbody currentlyHoldItem_ 
    {
        get { return currentlyHoldItem; }
    }

    [Header("Smooth movement")]
    public float smoothSpeed = 5.0f;

    public delegate void CheckCurRigidBody(GameObject rigidbody);
    public static CheckCurRigidBody CheckedCurRigidBody;

    private void OnEnable()
    {
        CheckBookInCurrentPlace.CurItemNullset += setCurrentItemNull;
    }


    private void OnDisable()
    {
        CheckBookInCurrentPlace.CurItemNullset -= setCurrentItemNull;
    }

    void Update()
    {
        ItemReach itemReach = GetComponent<ItemReach>();
        if (itemReach == null) 
        {
            CheckedCurRigidBody?.Invoke(null);
        }
        if (itemReach != null && itemReach.IsRaycastHit()) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) 
            {
                GameObject hitGameObject = hit.collider.gameObject;

                CheckedCurRigidBody?.Invoke(hitGameObject);

                if (Input.GetMouseButtonDown(0))
                {
                    Rigidbody hitRigidBody = hit.collider.GetComponent<Rigidbody>();
                    if (hitRigidBody != null && hitRigidBody.gameObject.layer == LayerMask.NameToLayer("InteractableItems"))
                    {
                        isDragging = true;
                        currentlyHoldItem = hitRigidBody;

                        //offset = currentlyHoldItem.transform.position.z;

                        currentlyHoldItem.isKinematic = true;

                    }
                }
            }
        }

        if (isDragging && currentlyHoldItem != null) 
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, offset));

            MoveWithCollisions(targetPosition);

            if (Input.GetMouseButtonUp(0)) 
            {
                isDragging = false;
                currentlyHoldItem.isKinematic = false;
                currentlyHoldItem = null;
            }
        }

    }

    private void MoveWithCollisions(Vector3 targetPosition) 
    {
        currentlyHoldItem.MovePosition(Vector3.Lerp(currentlyHoldItem.transform.position, targetPosition, smoothSpeed * Time.deltaTime));
    }

    private void setCurrentItemNull() 
    {
        isDragging = false;
        currentlyHoldItem.isKinematic = false;
        currentlyHoldItem = null;
    }

}
