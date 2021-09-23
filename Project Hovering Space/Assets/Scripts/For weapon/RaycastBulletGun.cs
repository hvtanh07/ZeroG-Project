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
    public enum Firemode
    {
        auto = 0,
        tap = 1,
    }
    [Header("Gun Properties")]
    public bool isFiring = false;
    public float fireRate = 11f;
    public int bulletsPerShot = 1;//5 
    public float angleSpread = 1.0f;//5   
    public float spreadModifier = 1.0f;
    public int ammoCount;
    public Firemode fireMode;
    [Range(0, 100)]
    public int clipSize = 30;
    bool reloading = false;
    [Space(10)]
    [Header("Location & Constraint object")]
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

    public override void StartFiring()
    {
        isFiring = true;
        acumulatedTime = 0f;
        recoil.Reset();
    }

    public override void UpdateWeapon(float deltaTime, bool holstered)
    {     
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
            if (fireMode == Firemode.tap)
            {
                isFiring = false;
            }          
        }
    }

    public void FireBullet()
    {
        Debug.DrawLine(raycastOrigin.position, raycastTarget.position,Color.red, 1f);
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
        float xNoise = Random.Range(-angleSpread * spreadModifier, angleSpread * spreadModifier);
        float yNoise = Random.Range(-angleSpread * spreadModifier, angleSpread * spreadModifier);
        float zNoise = Random.Range(-angleSpread * spreadModifier, angleSpread * spreadModifier);

        //Debug.Log(xNoise + " " + yNoise + " " + zNoise);
        // Convert Angle to Vector3
        return new Vector3(
          Mathf.Sin(2 * Mathf.PI * xNoise / 360),
          Mathf.Sin(2 * Mathf.PI * yNoise / 360),
          Mathf.Sin(2 * Mathf.PI * zNoise / 360)
        );   
    }

    public override void StopFiring()
    {
        isFiring = false;
    }
}
