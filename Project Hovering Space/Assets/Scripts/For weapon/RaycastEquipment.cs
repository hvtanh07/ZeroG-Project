using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastEquipment : MonoBehaviour
{
    [Header("Properties")]
    public string weaponName;
    public ActiveWeapon.WeaponSlot weaponSlot;
    public WeaponAnimationEvents equipmentEvents;
    public int AvailableQuantity;
    public Transform raycastTarget;
    public WeaponRecoil recoil;


    public virtual void UpdateWeapon(float deltaTime, bool holstered)
    {

       //input output holster
    }

    public virtual void FireBullet()
    {
        //firebullet
    }
}
