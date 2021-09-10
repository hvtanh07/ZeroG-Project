using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greanade : MonoBehaviour
{
    [Header("Properties")]
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    public bool beenThrown = true;
    public float countdown;
    [Space(10)]
    [Header("Effect")]
    public GameObject Effect;


    bool Exploded = false;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (beenThrown)
            countdown -= Time.deltaTime;
        if (countdown <= 0 && !Exploded)
        {
            Explode();
            Exploded = true;
        }
    }
    void Explode()
    {
        Instantiate(Effect, transform.position, transform.rotation);
        Collider[] collidersDestroy = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider objects in collidersDestroy)
        {
            //Playerhealth health = objects.GetComponent
            //if health
            //minus health calculated from position to object.position
        }

        Collider[] colliderstoMove = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider objects in colliderstoMove)
        {
            Rigidbody rb = objects.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }


        Destroy(gameObject);
    }
}
