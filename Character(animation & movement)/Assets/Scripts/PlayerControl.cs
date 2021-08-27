using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update   
    RaycastWeapon weapon;

    void Start()
    {               
        weapon = GetComponentInChildren<RaycastWeapon>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    weapon.StartFiring();
        //}
        //if (weapon.isFiring)
        //{
        //    weapon.UpdateFiring(Time.deltaTime);
        //}
        //weapon.UpdateBullets(Time.deltaTime);
        //if (Input.GetButtonUp("Fire1"))
        //{
        //    weapon.StopFiring();
        //}
    }
}
