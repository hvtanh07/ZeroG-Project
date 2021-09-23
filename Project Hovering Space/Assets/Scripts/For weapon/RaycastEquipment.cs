using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastEquipment : MonoBehaviour
{
    [Header("Equipment Properties")]
    public string weaponName;
    public ActiveWeapon.WeaponSlot weaponSlot;
    public ActiveWeapon.WeaponType weaponType;
    public int AvailableQuantity;
    public Transform raycastTarget;
    public WeaponRecoil recoil;
    public Animator rigController;
    public bool reloading = false;


    public virtual void UpdateWeapon(float deltaTime, bool holstered)
    {

       //input output holster
    }

    public virtual void StartFiring()
    {
        //firebullet
    }
    public virtual void StopFiring()
    {
        //Stopfiring
    }
    public virtual void SetReloading(bool value)
    {
       //set reloading state
    }
}
