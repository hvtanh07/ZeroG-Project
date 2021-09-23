using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGrenade : RaycastEquipment
{
    [Header("Grenade Properties")]
    public GameObject ThrowNade;

    public GameObject HandNade;

    public float ThrowForce = 40f;

    public float MinForce = 2f;
    public float MaxForce = 20f;
    public float Incresement = 3f;

    bool aiming;

    private void Awake()
    {
        recoil = GetComponent<WeaponRecoil>();
        ThrowForce = MinForce;
    }

    public bool isAiming()
    {
        return aiming;
    }

    public override void UpdateWeapon(float deltaTime, bool holstered)
    {       
        if (Input.GetKeyDown(KeyCode.P))
        {
            CancelThrow();
        }
        if (aiming)
        {
            if (ThrowForce < MaxForce)
                ThrowForce += Incresement * Time.deltaTime;
            else
                ThrowForce = MaxForce;
        }         
    }
    public override void StartFiring()
    {
        rigController.SetBool("cancel_nade", false);
        rigController.ResetTrigger("throw_nade");
        rigController.Play("Aim Grenade");
        aiming = true;
    }
    public override void StopFiring()
    {
        rigController.SetTrigger("throw_nade");
    }
    
    public void CancelThrow()
    {
        rigController.SetBool("cancel_nade", true);
    }
}
