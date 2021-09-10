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
        Tool = 3,
    }    
    public Transform[] weaponSlot;

    [Space(10)]
    [Header("Properties")]
    public Transform crosshairTarget;
    public Animator rigController;
    public Cinemachine.CinemachineFreeLook playerCamera;
    public WeaponAnimationEvents animationEvents;
    public RaycastEquipment[] equiped_weapons = new RaycastEquipment[4];
    int activeweaponIndex;
    public bool isHolstered = false;
    RaycastEquipment weapon;
    int SelectedWeapon = 0;
    int PrevSelectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        RaycastEquipment exitingWeapon = GetComponentInChildren<RaycastEquipment>();
        if (exitingWeapon)
        {
            Equip(exitingWeapon);
        }
    }

    public RaycastBulletGun GetActiveGun()
    {
        if (1 >= activeweaponIndex)
            return (RaycastBulletGun)GetWeapon(activeweaponIndex);
        else
            return (RaycastBulletGun)GetWeapon(-1);
    }

    public RaycastGrenade GetActiveNade()
    {
        return (RaycastGrenade)GetWeapon(2);
    }

    RaycastEquipment GetWeapon(int index)
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

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (SelectedWeapon > equiped_weapons.Length - 1)
                SelectedWeapon = 0;
            else
                SelectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (SelectedWeapon < 0)
                SelectedWeapon = equiped_weapons.Length - 1;
            else
                SelectedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectedWeapon = 3;
        }
        SelectingWeapon(SelectedWeapon);
        FindOutOfStockWeapon();
    }

    void SelectingWeapon(int weaponIndex)
    {
        if (PrevSelectedWeapon == weaponIndex) return;
        Debug.Log("activeweaponIndex: " + activeweaponIndex + "weaponIndex: " + (weaponIndex-1));
        if (GetActiveNade() && weaponIndex != 2)
            if (GetActiveNade().isAiming())
            {
                Debug.Log("butwhy");
                GetActiveNade().CancelThrow();
            }
        PrevSelectedWeapon = weaponIndex;
        SetActiveWeapon(weaponIndex);
    }

    public void Equip(RaycastEquipment newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var weapon = GetWeapon(weaponSlotIndex);
        if (weapon)
        {
            Destroy(weapon.gameObject);
        }
        weapon = newWeapon;
        weapon.raycastTarget = crosshairTarget;
        if (weapon.recoil)
        {
            weapon.recoil.playerCamera = playerCamera;
            weapon.recoil.rigController = rigController;
        }
        weapon.transform.SetParent(weaponSlot[weaponSlotIndex], false);
        weapon.rigController = rigController;
        equiped_weapons[weaponSlotIndex] = weapon;

        SetActiveWeapon(newWeapon.weaponSlot);

        //update UI ammo count
    }

    void FindOutOfStockWeapon()
    {
        for (int i = 0;i < equiped_weapons.Length; i++)
        {
            if (equiped_weapons[i])
                if (equiped_weapons[i].AvailableQuantity <= 0)
                    UnEquipOutOfStockWeapon(i);
        }
    }

    public void UnEquipOutOfStockWeapon(int weaponSlotIndex)
    {
        var weapon = GetWeapon(weaponSlotIndex);
        if (weapon)
        {
            Destroy(weapon.gameObject);
            Destroy(equiped_weapons[weaponSlotIndex]);
            
        }
        equiped_weapons[weaponSlotIndex] = null;
        rigController.Play("Unarmed");
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

    void SetActiveWeapon(int weaponSlot)
    {
        int holsterIndex = activeweaponIndex;
        int activateIndex = weaponSlot;

        if (holsterIndex == activateIndex)
        {
            holsterIndex = -1;
        }

        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
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

    public IEnumerator putAllweaponAway()
    {
        isHolstered = true;
        var weapon = GetWeapon(activeweaponIndex);
        if (weapon)
        {
            weapon.StopFiring();
            rigController.SetBool("holster_weapon", true);
            //do
            //{
            //    yield return new WaitForEndOfFrame();
            //} while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);

            //Used when RigLayer animator use Animate Physics Update Mode.
            yield return new WaitForSeconds(rigController.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    public IEnumerator HolsterWeapon(int index)
    {
        isHolstered = true;
        var weapon = GetWeapon(index);       
        if (weapon)
        {
            weapon.StopFiring();
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
