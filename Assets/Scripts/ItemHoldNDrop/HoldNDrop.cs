using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoldNDrop : MonoBehaviour
{
    [SerializeField]
    private Material outlineMaterial;

    private bool isDragging = false;
    private Rigidbody currentlyHoldItem;
    private MeshRenderer hitItemRenderer;
    private Vector3 offset;
    private Material[] originalMaterials;

    [Header("Smooth movement")]
    public float smoothSpeed = 5.0f;

    private GameObject lastHoldItem;

    private bool lastIsColored = false;

    private void Start()
    {
        lastHoldItem = new GameObject("Cool GameObject made from Code");
        lastHoldItem.AddComponent<MeshRenderer>();
        originalMaterials = new Material[1];
    }

    void Update()
    {
        ItemReach itemReach = GetComponent<ItemReach>();

        if (itemReach != null && itemReach.IsRaycastHit()) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) 
            {
                GameObject hitGameObject = hit.collider.gameObject;
                if (hitGameObject != lastHoldItem)
                {
                    MeshRenderer lastHoldItemRenderer = lastHoldItem.GetComponent<MeshRenderer>();
                    lastHoldItemRenderer.materials = originalMaterials;
                    lastIsColored = false;
                }

                if (hitGameObject.gameObject.layer == LayerMask.NameToLayer("Items") && !lastIsColored)
                {
                    hitItemRenderer = hitGameObject.gameObject.GetComponent<MeshRenderer>();
                    originalMaterials = hitItemRenderer.materials;
                    Material[] tmats = new Material[originalMaterials.Length + 1];

                    for (int i = 0; i < originalMaterials.Length; i++)
                    {
                        tmats[i] = originalMaterials[i];
                    }
                    tmats[tmats.Length - 1] = outlineMaterial;

                    hitItemRenderer.materials = tmats;

                    lastHoldItem = hitGameObject;
                    lastIsColored = true;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Rigidbody hitRigidBody = hit.collider.GetComponent<Rigidbody>();
                    if (hitRigidBody != null && hitRigidBody.gameObject.layer == LayerMask.NameToLayer("Items"))
                    {
                        isDragging = true;
                        currentlyHoldItem = hitRigidBody;

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
