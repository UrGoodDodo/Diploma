using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNDrop : MonoBehaviour
{

    private bool isDragging = false;
    private Rigidbody currentlyHoldItem;
    private Vector3 offset;
    private int originalLayer;

    [Header("Smooth movement")]
    public float smoothSpeed = 5.0f;

    void Update()
    {
        ItemReach itemReach = GetComponent<ItemReach>();

        if (itemReach != null && itemReach.IsRaycastHit()) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) 
            {
                if (Input.GetMouseButtonDown(0)) 
                {
                    Rigidbody hitRigidBody = hit.collider.GetComponent<Rigidbody>();
                    if (hitRigidBody != null) 
                    {
                        isDragging = true;
                        currentlyHoldItem = hitRigidBody;
                        originalLayer = currentlyHoldItem.gameObject.layer;

                        int temporaryLayer = LayerMask.NameToLayer("TemporaryLayer");
                        currentlyHoldItem.gameObject.layer = temporaryLayer;

                        offset = currentlyHoldItem.transform.position - hit.point;

                        currentlyHoldItem.isKinematic = true;
                    }
                }
            }
        }

        if (isDragging && currentlyHoldItem != null) 
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, Camera.main.transform.position.y));
            MoveWithCollisions(targetPosition);
            if (Input.GetMouseButtonUp(0)) 
            {
                currentlyHoldItem.gameObject.layer = originalLayer;
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
}
