using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
        public int bounce;
    }

    public ActiveWeapon.WeaponSlot weaponSlot;
    public bool isFiring = false;
    public float fireRate = 11f;
    public float bulletSpeed = 1000f;
    public float bulletDrop = 0.0f;
    public int maxBounces = 0;
    public float MaxlifeTime = 3.0f;

    public ParticleSystem MuzzleFlash;
    public ParticleSystem HitEffect;
    public TrailRenderer tracerEffect;
    public string weaponName;

    public int ammoCount;
    public int clipSize = 30;
    bool reloading = false;

    public AnimationClip weaponAnimation;
    public Transform raycastOrigin;
    public Transform raycastTarget;
    public WeaponRecoil recoil;
    public GameObject magazine;
     
    Ray ray;
    RaycastHit hitInfo;
    float acumulatedTime;
    List<Bullet> bullets = new List<Bullet>();

    private void Awake()
    {
        ammoCount = clipSize;
        recoil = GetComponent<WeaponRecoil>();
    }

    public void SetReloading(bool value)
    {
        reloading = value;
    }

    Vector3 GetPosition(Bullet bullet)
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return bullet.initialPosition + bullet.initialVelocity * bullet.time + 0.5f * gravity * bullet.time * bullet.time;
    }

    Bullet createBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.tracer = Instantiate(tracerEffect, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        bullet.bounce = maxBounces;
        return bullet;
    }

    public void StartFiring()
    {
        isFiring = true;
        acumulatedTime = 0f;
        recoil.Reset(); 

    }

    public void UpdateWeapon(float deltaTime, bool holstered)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!holstered)
                StartFiring();
        }
        if (isFiring)
        {
            UpdateFiring(deltaTime);
        }
        UpdateBullets(deltaTime);
        if (Input.GetButtonUp("Fire1"))
        {
            StopFiring();
        }
    }
     
    public void UpdateFiring(float deltaTime)
    {
        acumulatedTime += deltaTime;
        float fireInteval = 1.0f / fireRate;
        while (acumulatedTime >= 0.0f)
        {
            FireBullet();
            acumulatedTime -= fireInteval;
        }
    }

    public void UpdateBullets(float deltaTime)
    {       
        SimulateBullets(deltaTime);
        Destroybullets();
    }

    void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RayCastSegment(p0, p1, bullet);
        });
    }

    void Destroybullets()
    {
        bullets.RemoveAll(bullet => bullet.time >= MaxlifeTime);
    }

    void RayCastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;

        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
            //Instantiate(HitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
            HitEffect.transform.position = hitInfo.point;
            HitEffect.transform.forward = hitInfo.normal;
            HitEffect.Emit(1);

            //bullet.tracer.transform.position = hitInfo.point;
            bullet.time = MaxlifeTime;
            end = hitInfo.point;

            // Bullet ricochet;
            if (bullet.bounce > 0)
            {
                bullet.time = 0;
                bullet.initialPosition = hitInfo.point;
                bullet.initialVelocity = Vector3.Reflect(bullet.initialVelocity, hitInfo.normal);
                bullet.bounce--;
            }

            //colision impulse
            var rb2d = hitInfo.collider.GetComponent<Rigidbody>();
            if (rb2d)
            {
                rb2d.AddForceAtPosition(ray.direction * 20, hitInfo.point, ForceMode.Impulse);
            }
        }
        //else
        //{
        //    bullet.tracer.transform.position = end;
        //}
        bullet.tracer.transform.position = end;
    
    }
    private void FireBullet()
    {
        if (ammoCount <=0 || reloading)
        {
            return;
        }
        ammoCount--;
        MuzzleFlash.Emit(1);

        Vector3 velocity = (raycastTarget.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = createBullet(raycastOrigin.position, velocity);
        bullets.Add(bullet);
        //ray.origin = raycastOrigin.position;
        //ray.direction = raycastTarget.position - raycastOrigin.position;

        //var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        //tracer.AddPosition(ray.origin);

        recoil.GenerateRecoid(weaponName);
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
