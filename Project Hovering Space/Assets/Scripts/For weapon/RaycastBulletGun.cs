using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBulletGun : RaycastEquipment
{

    public enum bulletType
    {
        defaultType = 0,
        Greanade = 2,
    }
    [Header("Properties")]
    public bool isFiring = false;
    public float fireRate = 11f;
    public int bulletsPerShot = 1;//5 
    public float angleSpread = 1.0f;//5   
    public int ammoCount;
    [Range(0, 100)]
    public int clipSize = 30;
    bool reloading = false;
    [Space(10)]
    [Header("Location & Constraint object")]
    //public AnimationClip weaponAnimation;
    public Transform raycastOrigin;
    public bulletScript bullets;
    public GameObject magazine;
    [Space(10)]
    [Header("Effect")]
    public ParticleSystem MuzzleFlash;

    float acumulatedTime;
    Animator gunanimation;

    private void Awake()
    {
        gunanimation = GetComponent<Animator>();
        ammoCount = clipSize;
        recoil = GetComponent<WeaponRecoil>();
    }

    public void SetReloading(bool value)
    {
        reloading = value;
        if (reloading)
        {
            //Start reloading animation;
            if (gunanimation)
                gunanimation.Play(weaponName + "_reload", 0, 0.0f);
        }
    }

    public void StartFiring()
    {
        if(weaponSlot == ActiveWeapon.WeaponSlot.Grenade)
        {
            //Aiming the grenade
            return;
        }
        isFiring = true;
        acumulatedTime = 0f;
        recoil.Reset(); 

    }

    public override void UpdateWeapon(float deltaTime, bool holstered)
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

    public void WeaponReload()
    {
        isFiring = false;
    }
    public override void FireBullet()
    {
        if (ammoCount <=0 || reloading)
        {
            return;
        }
        ammoCount--;
        MuzzleFlash.Emit(1);
        if(gunanimation)
            gunanimation.Play(weaponName + "_recoil",0,0.0f);
        Vector3 angle = (raycastTarget.position - raycastOrigin.position).normalized;
        for (int i = 0; i < bulletsPerShot; i++)
        {
            bulletScript bulletout = Instantiate(bullets, raycastOrigin.position, raycastOrigin.transform.rotation);
            bulletout.initVarible(raycastOrigin.position, (angle += AddNoiseOnAngle()).normalized);
        }
        recoil.GenerateRecoid(weaponName);
    }

    Vector3 AddNoiseOnAngle()
    {
        // Find random angle between min & max inclusive
        float xNoise = Random.Range(-angleSpread, angleSpread);
        float yNoise = Random.Range(-angleSpread, angleSpread);
        float zNoise = Random.Range(-angleSpread, angleSpread);

        //Debug.Log(xNoise + " " + yNoise + " " + zNoise);
        // Convert Angle to Vector3
        return new Vector3(
          Mathf.Sin(2 * Mathf.PI * xNoise / 360),
          Mathf.Sin(2 * Mathf.PI * yNoise / 360),
          Mathf.Sin(2 * Mathf.PI * zNoise / 360)
        );   
    }

        public void StopFiring()
    {
        isFiring = false;
    }
}
