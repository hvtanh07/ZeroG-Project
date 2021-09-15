using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCrossHairTarget : MonoBehaviour
{
    public Camera Aimcamera;
    public Transform PrevPos;

    Ray ray;
    RaycastHit hitInfo;

    // Start is called before the first frame update

    // Update is called once per frame
    void OnEnable()
    {
        //Aimcamera.transform.LookAt(PrevPos);
        Debug.Log("Ran");
    }
    void Update()
    {
        ray.origin = Aimcamera.transform.position;
        ray.direction = Aimcamera.transform.forward;
        //Physics.Raycast(ray, out hitInfo);
        //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 0.5f);
        transform.position = hitInfo.point;
        if (Physics.Raycast(ray, out hitInfo))
        {
            transform.position = hitInfo.point;
        }
        else
        {
            transform.position = ray.origin + ray.direction * 1000.0f;
        }
    }
}
