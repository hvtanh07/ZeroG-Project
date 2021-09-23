using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCrossHairTarget : MonoBehaviour
{
    public Camera Aimcamera;
    public Transform PrevPos;
    public LayerMask ignoreAimLayer;
    Ray ray;
    RaycastHit hitInfo;
  
    void Update()
    {
        ray.origin = Aimcamera.transform.position;
        ray.direction = Aimcamera.transform.forward;
        transform.position = hitInfo.point;
        if (Physics.Raycast(ray, out hitInfo, 1000f, ~ignoreAimLayer))
        {
            transform.position = hitInfo.point;
        }
        else
        {
            transform.position = ray.origin + ray.direction * 1000.0f;
        }
    }
}
