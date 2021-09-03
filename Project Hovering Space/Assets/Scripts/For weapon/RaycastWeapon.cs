using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{

    public enum bulletType
    {
        defaultType = 0,
        Greanade = 2,
    }
    [Header("Properties")]
    public ActiveWeapon.WeaponSlot weaponSlot;

    public bool isFiring = false;
    public float fireRate = 11f;
    public int bulletsPerShot = 1;//5 
    public float minSpread = -1.0f;//-5 for shotgun
    public float maxSpread = 1.0f;//5
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

    [Header("Effect")]
    public ParticleSystem MuzzleFlash;
    public ParticleSystem HitEffect;

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
        Vector3 angle = (raycastTarget.position - raycastOrigin.position).normalized;
        if (1 == bulletsPerShot)
        {
            bulletScript bulletout = Instantiate(bullets, raycastOrigin.position, raycastOrigin.transform.rotation);
            bulletout.initVarible(raycastOrigin.position, angle);
        }
        else
        {
            for(int i = 0;i< bulletsPerShot; i++)
            {
                bulletScript bulletout = Instantiate(bullets, raycastOrigin.position, raycastOrigin.transform.rotation);
                bulletout.initVarible(raycastOrigin.position, angle += AddNoiseOnAngle(minSpread, maxSpread));
            }
        }

        recoil.GenerateRecoid(weaponName);
    }

    Vector3 AddNoiseOnAngle(float min, float max)
    {
        // Find random angle between min & max inclusive
        float xNoise = Random.Range(min, max);
        float yNoise = Random.Range(min, max);
        float zNoise = Random.Range(min, max);

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
