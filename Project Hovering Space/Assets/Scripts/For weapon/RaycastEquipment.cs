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
    //public GrenadeThrow throwNade;
    //public WeaponAnimationEvents animationEvents;


    public virtual void UpdateWeapon(float deltaTime, bool holstered)
    {

       //input output holster
    }

    public virtual void FireBullet()
    {
        //firebullet
    }
    public virtual void StopFiring()
    {

    }
}
