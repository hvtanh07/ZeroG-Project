using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : MonoBehaviour
{
    [Header("Properties")]
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;   
    public float countdown;
    //public GameObject Bullet;
    [Space(10)]
    [Header("Effect")]
    public GameObject Effect;

    bool Exploded = false;
    void Start()
    {
        countdown = delay;
    }
    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !Exploded)
        {
            Explode();
            Exploded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Explode();
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
