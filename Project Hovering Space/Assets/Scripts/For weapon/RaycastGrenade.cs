using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGrenade : RaycastEquipment
{
    public GameObject ThrowNade;

    private void Awake()
    {
        recoil = GetComponent<WeaponRecoil>();
    }

    public override void UpdateWeapon(float deltaTime, bool holstered)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            
        }
        if (Input.GetButtonUp("Fire1"))
        {
            
        }
    }

    void Aimdirection()
    {
        //gunanimation.Play("Aim Grenade", 0, 0.0f);
    }
    void Throw()
    {

    }
     
}
