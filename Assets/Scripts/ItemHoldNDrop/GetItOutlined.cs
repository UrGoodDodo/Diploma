using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItOutlined : MonoBehaviour
{

    [SerializeField]
    private Material outlineMaterial;

    private GameObject oldGameObj = null;
    private Material[] originalMaterials;

    private void OnEnable()
    {
        TagOrLayerChecker.OutLinedIt += MakeItOutlined;
        TagOrLayerChecker.DisableOldOutline += DisableOutlineOnOld;
    }

    private void OnDisable()
    {
        TagOrLayerChecker.OutLinedIt -= MakeItOutlined;
        TagOrLayerChecker.DisableOldOutline -= DisableOutlineOnOld;
    }

    void MakeItOutlined(GameObject gameobject) 
    {
        if (oldGameObj == null)
        {
            OutLineCurrent(gameobject);

            oldGameObj = gameobject;
        }
        else 
        {
            DisableOutlineOnOld();
            OutLineCurrent(gameobject);
        }
    }

    void OutLineCurrent(GameObject gameobject) 
    {
        MeshRenderer hitItemRenderer = gameobject.GetComponent<MeshRenderer>();
        originalMaterials = hitItemRenderer.materials;
        Material[] tmats = new Material[originalMaterials.Length + 1];
        for (int i = 0; i < originalMaterials.Length; i++)
        {
            tmats[i] = originalMaterials[i];
        }
        tmats[tmats.Length - 1] = outlineMaterial;
        hitItemRenderer.materials = tmats;
    }

    void DisableOutlineOnOld() 
    {
        MeshRenderer lastHoldItemRenderer = oldGameObj.GetComponent<MeshRenderer>();
        lastHoldItemRenderer.materials = originalMaterials;
    }
}
