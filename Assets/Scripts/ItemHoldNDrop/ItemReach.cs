using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReach : MonoBehaviour
{
    public float reachDsitance = 0.01f;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, reachDsitance))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            //Debug.Log("Object hit " + hit.collider.gameObject.name);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * reachDsitance, Color.green);
        }
    }

    public bool IsRaycastHit() 
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        return Physics.Raycast(ray, out hit, reachDsitance);
    }
}
