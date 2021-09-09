using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : MonoBehaviour
{
    public RaycastGrenade nade_drop;
    public RaycastBulletGun gun_drop;

    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if (activeWeapon)
        {
            if (gun_drop)
            {
                RaycastEquipment newGun = Instantiate(gun_drop);
                activeWeapon.Equip(newGun);
            }
            if (nade_drop)
            {
                RaycastEquipment newNade = Instantiate(nade_drop);
                activeWeapon.Equip(newNade);
            }
            //Destroy(gameObject);
        }
    }
}
