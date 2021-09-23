using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;



public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1,
        Equipment1 = 2,
        Equipment2 = 3,
    }
    public enum WeaponType
    {
        DefaultGun = 0,
        GrenadeLauncher = 1,
        Grenade = 2,       
    }
    public Transform[] weaponSlot;

    [Space(10)]
    [Header("Properties")]
    public Transform crosshairTarget;
    public Transform scopeTarget;
    public Animator rigController;
    public PlayerAiming playerAiming;
    public WeaponAnimationEvents animationEvents;
    public RaycastEquipment[] equiped_weapons = new RaycastEquipment[4];
    int activeweaponIndex;
    public bool isHolstered = false;
    RaycastEquipment weapon;
    int SelectedWeapon = 0;
    int PrevSelectedWeapon = 0;

    //Transform Target;

    // Start is called before the first frame update
    void Start()
    {
        //Target = crosshairTarget;
        RaycastEquipment exitingWeapon = GetComponentInChildren<RaycastEquipment>();
        if (exitingWeapon)
        {
            Equip(exitingWeapon);
        }
    }

    public RaycastEquipment GetActiveWeapon()
    {
        if (GetWeapon(activeweaponIndex))            
                return GetWeapon(activeweaponIndex);
        return null;
    }
    public RaycastBulletGun GetActiveGun()
    {
        if (GetWeapon(activeweaponIndex))
            if(GetWeapon(activeweaponIndex).weaponType == WeaponType.DefaultGun)
                return (RaycastBulletGun)GetWeapon(activeweaponIndex);
        return null;
    }
    public RaycastGrenade GetActiveNade()
    {
        if (GetWeapon(activeweaponIndex))
            if(GetWeapon(activeweaponIndex).weaponType == WeaponType.Grenade)
                return (RaycastGrenade)GetWeapon(activeweaponIndex);
        return null;
    }
    public RaycastNadeLauncher GetActiveGrenadeLauncher()//Change to Grenade Launcher
    {
        if (GetWeapon(activeweaponIndex))
            if(GetWeapon(activeweaponIndex).weaponType == WeaponType.GrenadeLauncher)
            return (RaycastNadeLauncher)GetWeapon(activeweaponIndex);
        return null;
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
            if (playerAiming.isScoping)
            {
                weapon.raycastTarget = scopeTarget;
            }
            else
            {
                weapon.raycastTarget = crosshairTarget;
            }
            weapon.UpdateWeapon(Time.deltaTime, isHolstered);
        }
        FindOutOfStockWeapon();
    }

    #region Input Control
    public void ScrollWheel(InputAction.CallbackContext value)
    {
        float valueScroll = value.ReadValue<float>();
        if (valueScroll < 0)
            do
            {
                if (SelectedWeapon < 0)
                    SelectedWeapon = equiped_weapons.Length - 1;
                else
                    SelectedWeapon--;
            }
            while (!GetWeapon(SelectedWeapon));
        if (valueScroll > 0)
            do
            {
                if (SelectedWeapon > equiped_weapons.Length - 1)
                    SelectedWeapon = 0;
                else
                    SelectedWeapon++;
            }
            while (!GetWeapon(SelectedWeapon));
        SelectingWeapon(SelectedWeapon);
    }
    public void Shoot(InputAction.CallbackContext value)
    {
        var weapon = GetWeapon(activeweaponIndex);
        if (value.performed)
        {
            if(!isHolstered)
                weapon.StartFiring();
        }

        if (value.canceled)
        {
            weapon.StopFiring();
        }
    }
    public void ToggleHolster(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            ToggleActiveWeapon();
        }
    }
    public void Primary(InputAction.CallbackContext value)
    {   
        if (value.performed)
        {
            SelectedWeapon = 0;
        }
    }
    public void Secondary(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            SelectedWeapon = 1;
        }
    }
    public void Equipement1(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            SelectedWeapon = 2;
        }
    }
    public void Equipement2(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            SelectedWeapon = 3;
        }
    }
    #endregion

    void SelectingWeapon(int weaponIndex)
    {
        if (!GetWeapon(weaponIndex))
            return;
        if (PrevSelectedWeapon == weaponIndex) return;

        if (GetActiveNade() && weaponIndex != 2)
            if (GetActiveNade().isAiming())
            {
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
            weapon.recoil.playerAiming = playerAiming;
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
            playerAiming.isAiming = false;
            playerAiming.isScoping = false;
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
            playerAiming.isAiming = false;
            playerAiming.isScoping = false;
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
