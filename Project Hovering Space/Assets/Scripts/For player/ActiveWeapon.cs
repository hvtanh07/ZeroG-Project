using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1,
        Grenade = 2,
    }    
    public Transform[] weaponSlot;

    [Space(10)]
    [Header("Properties")]
    public Transform crosshairTarget;
    public Animator rigController;
    public Cinemachine.CinemachineFreeLook playerCamera;

    RaycastWeapon[] equiped_weapons = new RaycastWeapon[2];
    int activeweaponIndex;
    public bool isHolstered = false;
    int Holsterindex;

    RaycastWeapon weapon;


    // Start is called before the first frame update
    void Start()
    {
        RaycastWeapon exitingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (exitingWeapon)
        {
            Equip(exitingWeapon);
        }
    }

    public RaycastWeapon GetActiveWeapon()
    {
        return GetWeapon(activeweaponIndex);
    }

    RaycastWeapon GetWeapon(int index)
    {
        if (index < 0 || index >= weaponSlot.Length)
        {
            return null;
        }
        return equiped_weapons[index];
    }

    //Update is called once per frame
    void Update()
    {
        var weapon = GetWeapon(activeweaponIndex);
        if (weapon)
        {
            weapon.UpdateWeapon(Time.deltaTime, isHolstered);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleActiveWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(WeaponSlot.Primary);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(WeaponSlot.Secondary);
        }
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var weapon = GetWeapon(weaponSlotIndex);
        if (weapon)
        {
            Destroy(weapon.gameObject);
        }
        weapon = newWeapon;
        weapon.raycastTarget = crosshairTarget;
        weapon.recoil.playerCamera = playerCamera;
        weapon.recoil.rigController = rigController;
        weapon.transform.SetParent(weaponSlot[weaponSlotIndex], false);

        equiped_weapons[weaponSlotIndex] = weapon;

        SetActiveWeapon(newWeapon.weaponSlot);

        //update UI ammo count
    }

    void ToggleActiveWeapon()
    {
        bool isHolsterd = rigController.GetBool("holster_weapon");
        if (isHolsterd)
        {
            StartCoroutine(ActivateWeapon(activeweaponIndex));
        }
        else
        {
            StartCoroutine(HolsterWeapon(activeweaponIndex));
        }
    }

    void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int holsterIndex = activeweaponIndex;
        int activateIndex = (int)weaponSlot;

        if (holsterIndex == activateIndex)
        {
            holsterIndex = -1;
        }

        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }

    IEnumerator SwitchWeapon(int holsterIndex, int activeIndex)
    {
       
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        
        yield return StartCoroutine(ActivateWeapon(activeIndex));
        activeweaponIndex = activeIndex;
    }

    IEnumerator HolsterWeapon(int index)
    {
        isHolstered = true;
        var weapon = GetWeapon(index);       
        if (weapon)
        {
            rigController.SetBool("holster_weapon", true);
            //do
            //{
            //    yield return new WaitForEndOfFrame();
            //} while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);

            //Used when RigLayer animator use Animate Physics Update Mode.
            yield return new WaitForSeconds(rigController.GetCurrentAnimatorStateInfo(0).length);
        }

    }

    IEnumerator ActivateWeapon(int index)
    {
        var weapon = GetWeapon(index);
        if (weapon)
        {
            rigController.SetBool("holster_weapon", false);
            rigController.Play("Equip_" + weapon.weaponName);
            //do
            //{
            //    yield return new WaitForEndOfFrame();
            //} while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);

            //Used when RigLayer animator use Animate Physics Update Mode.
            yield return new WaitForSeconds(rigController.GetCurrentAnimatorStateInfo(0).length);
            isHolstered = false;
        }
    }
}
