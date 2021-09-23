using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastNadeLauncher : RaycastEquipment
{
    public enum bulletType
    {
        defaultType = 0,
        Greanade = 2,
    }
    [Header("Gun Properties")]
    public bool isFiring = false;
    public float fireRate = 11f;   
    public int ammoCount;
    [Range(0, 100)]
    public int clipSize = 30;
    public float ShootingForce = 10f;
    bool reloading = false;
    [Space(10)]
    [Header("Location & Constraint object")]
    public Transform raycastOrigin;
    public GameObject bullets;
    public GameObject magazine;
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
        if (ammoCount <= 0 || reloading)
        {
            return;
        }
        ammoCount--;
        if (gunanimation)
            gunanimation.Play(weaponName + "_recoil", 0, 0.0f);
        recoil.GenerateRecoid(weaponName);
        GameObject bullet = Instantiate(bullets, raycastOrigin.position, raycastOrigin.rotation);
        Vector3 direction = (raycastTarget.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody>().AddForce(direction * ShootingForce, ForceMode.VelocityChange);
    }
}
