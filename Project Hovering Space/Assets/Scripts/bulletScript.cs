using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    float time;
    Vector3 initialPosition;
    Vector3 initialVelocity;
    TrailRenderer tracer;
    int bounce;

    [Range(0f, 1f)]
    public float ricochetChance;
    public float minRicochetAngle;
    public float bulletSpeed = 1000f;
    public float bulletDrop = 0.0f;
    public int maxBounces = 0;
    public float MaxlifeTime = 3.0f;

    public ParticleSystem HitEffect;
    public TrailRenderer tracerEffect;

    Ray ray;
    RaycastHit hitInfo;

    private void Start()
    {
        Destroy(gameObject, MaxlifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateBullets(Time.deltaTime);
        SimulateBullets(Time.deltaTime);
    }
    Vector3 GetPosition()
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return initialPosition + initialVelocity * time + 0.5f * gravity * time * time;
    }
    public void initVarible(Vector3 position, Vector3 target)
    {
        initialPosition = position;
        initialVelocity = (target - position).normalized * bulletSpeed;
        time = 0.0f;
        tracer = Instantiate(tracerEffect, position, Quaternion.identity);
        tracer.AddPosition(position);
        bounce = maxBounces;
    }

    void SimulateBullets(float deltaTime)
    {
        Vector3 p0 = GetPosition();
        this.time += deltaTime;
        Vector3 p1 = GetPosition();
        RayCastSegment(p0, p1);
    }

    void RayCastSegment(Vector3 start, Vector3 end)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;

        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            Debug.Log("hit");
            //Destroy(tracer);
            HitEffect.transform.position = hitInfo.point;
            HitEffect.transform.forward = hitInfo.normal;
            HitEffect.Emit(1);

            //bullet.tracer.transform.position = hitInfo.point;
            time = MaxlifeTime;
            end = hitInfo.point;

            // Bullet ricochet;
            if (bounce > 0)
            {             
                // calculate the bullet angle from the normal it hit
                var bulletAngle = Vector3.Angle(hitInfo.normal, -ray.direction);

                if (bulletAngle > minRicochetAngle)
                {
                    if (Random.value < ricochetChance)
                    {
                        time = 0f;
                        initialPosition = hitInfo.point;
                        // maybe add some randomness on the reflect for some more realistic ricochet behaviour
                        initialVelocity = Vector3.Reflect(initialVelocity, hitInfo.normal);
                        bounce--;
                    }
                }
            }

            //colision impulse
            var rb2d = hitInfo.collider.GetComponent<Rigidbody>();
            if (rb2d)
            {
                rb2d.AddForceAtPosition(ray.direction * 20, hitInfo.point, ForceMode.Impulse);
            }
        }

        if (tracer)
            tracer.transform.position = end;
    }
}
