using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public ActiveWeapon.WeaponSlot weaponSlot;
    public bool isFiring = false;
    public float fireRate = 11f;

    public ParticleSystem MuzzleFlash;
    public ParticleSystem HitEffect;
    public string weaponName;

    public int ammoCount;
    public int clipSize = 30;
    bool reloading = false;

    //public AnimationClip weaponAnimation;
    public Transform raycastOrigin;
    public Transform raycastTarget;
    public bulletScript bullets;
    public WeaponRecoil recoil;
    public GameObject magazine;
     
    float acumulatedTime;

    private void Awake()
    {
        ammoCount = clipSize;
        recoil = GetComponent<WeaponRecoil>();
    }

    public void SetReloading(bool value)
    {
        reloading = value;
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
        if (Input.GetButtonUp("Fire1"))
        {
            StopFiring();
        }
        if (isFiring)
        {
            UpdateFiring(deltaTime);
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

   
    private void FireBullet()
    {
        if (ammoCount <=0 || reloading)
        {
            return;
        }
        ammoCount--;
        MuzzleFlash.Emit(1);
        //Vector3 velocity = (raycastTarget.position - raycastOrigin.position).normalized * bulletSpeed;
        bulletScript bulletout = Instantiate(bullets, raycastOrigin.position, raycastOrigin.transform.rotation);
        bulletout.initVarible(raycastOrigin.position, raycastTarget.position);
        //var bullet = createBullet(raycastOrigin.position, velocity);
        //bullets.Add(bullet);

        recoil.GenerateRecoid(weaponName);
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
