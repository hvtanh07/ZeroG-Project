using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGrenade : RaycastEquipment
{
    public GameObject ThrowNade;

    public GameObject HandNade;

    public float ThrowForce = 40f;

    private void Awake()
    {
        recoil = GetComponent<WeaponRecoil>();
    }

    public override void UpdateWeapon(float deltaTime, bool holstered)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(!holstered)
                Aimdirection();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            Throw();
        }
    }

    void Aimdirection()
    {
        Debug.Log("aim");
        rigController.Play("Aim Grenade");
        //throwNade.AimNade();
    }
    void Throw()
    {
        Debug.Log("throw");
        rigController.SetTrigger("throw_nade");
        //throwNade.ThrowNade();
    }
     
}
