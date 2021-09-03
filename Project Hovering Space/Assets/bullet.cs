using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float time;
    public Vector3 initialPosition;
    public Vector3 initialVelocity;
    public TrailRenderer tracer;
    public int bounce;

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
        //Destroy(gameObject, MaxlifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBullets(Time.deltaTime);
    }
    Vector3 GetPosition()
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return initialPosition + initialVelocity * time + 0.5f * gravity * time * time;
    }
    public void initVarible(Vector3 position, Vector3 target)
    {
        //bullet bullet = new bullet();
        initialPosition = position;
        initialVelocity = (target - position).normalized * bulletSpeed;
        time = 0.0f;
        tracer = Instantiate(tracerEffect, position, Quaternion.identity);
        tracer.AddPosition(position);
        bounce = maxBounces;
        //return bullet;
    }
    void UpdateBullets(float deltaTime)
    {
        SimulateBullets(deltaTime);
        Destroybullets();
    }

    void SimulateBullets(float deltaTime)
    {
        Vector3 p0 = GetPosition();
        this.time += deltaTime;
        Vector3 p1 = GetPosition();
        RayCastSegment(p0, p1);
    }

    void Destroybullets()
    {
        if (time>=MaxlifeTime)
            Destroy(gameObject);
        //bullets.RemoveAll(bullet => bullet.time >= MaxlifeTime);
    }

    void RayCastSegment(Vector3 start, Vector3 end)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;

        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            HitEffect.transform.position = hitInfo.point;
            HitEffect.transform.forward = hitInfo.normal;
            HitEffect.Emit(1);

            //bullet.tracer.transform.position = hitInfo.point;
            time = MaxlifeTime;
            end = hitInfo.point;

            // Bullet ricochet;
            if (bounce > 0)
            {
                time = 0;
                initialPosition = hitInfo.point;
                initialVelocity = Vector3.Reflect(initialVelocity, hitInfo.normal);
                bounce--;
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
