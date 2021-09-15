using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
    Camera maincamera;

    Ray ray;
    RaycastHit hitInfo;

    // Start is called before the first frame update
    void Start()
    {
        maincamera = Camera.main;
        //AimingCam = Camera.;
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = maincamera.transform.position;
        ray.direction = maincamera.transform.forward;
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
