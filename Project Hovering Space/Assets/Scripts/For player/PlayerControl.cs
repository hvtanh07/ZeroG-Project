using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update   
    public Transform targetLand;

    public float power = 15000f;
    public float torque = 500f;
    public float gravity = 9.81f;

    public bool autoOrient = false;
    public float autoOrientSpeed = 1f;

    Rigidbody body;

    void Start()
    {               
        body = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        ProcessGravity();
    }
    void ProcessGravity()
    {
        Vector3 diff = transform.position - targetLand.position;
        body.AddForce(-diff.normalized * gravity * (body.mass));

        if (autoOrient)
        {
            AutoOrient(-diff);
        }
    }
    void AutoOrient(Vector3 down)
    {
        Quaternion orientationDirection = Quaternion.FromToRotation(-transform.up, down) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, orientationDirection, Time.deltaTime * autoOrientSpeed);
    }
}
