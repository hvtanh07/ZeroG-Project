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
    RaycastEquipment[] equiped_weapons = new RaycastEquipment[4];
    int activeweaponIndex;
    public bool isHolstered = false;
    RaycastEquipment weapon;


    // Start is called before the first frame update
    void Start()
    {
        RaycastEquipment exitingWeapon = GetComponentInChildren<RaycastEquipment>();
        if (exitingWeapon)
        {
            Equip(exitingWeapon);
        }
    }

    public RaycastEquipment GetActiveGun()
    {
        if (1 >= activeweaponIndex)
            return GetWeapon(activeweaponIndex);
        else
            return GetWeapon(-1);
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(WeaponSlot.Primary);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(WeaponSlot.Secondary);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveWeapon(WeaponSlot.Grenade);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetActiveWeapon(WeaponSlot.Tool);
        }
        
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
        weapon.recoil.playerCamera = playerCamera;
        weapon.recoil.rigController = rigController;
        weapon.equipmentEvents = animationEvents;
        weapon.transform.SetParent(weaponSlot[weaponSlotIndex], false);

        equiped_weapons[weaponSlotIndex] = weapon;

        SetActiveWeapon(newWeapon.weaponSlot);

        //update UI ammo count
    }

    public void UnEquip(int weaponSlotIndex)
    {

        Debug.Log(weaponSlotIndex);
        var weapon = GetWeapon(weaponSlotIndex);
        if (weapon)
        {
            Destroy(equiped_weapons[weaponSlotIndex]);
        }
        equiped_weapons[weaponSlotIndex] = null;
        for (int i = 0; equiped_weapons[i] != null; i++)
        {
            switch (i) {
                case 0:
                    SetActiveWeapon(WeaponSlot.Primary);
                    break;
                case 1:
                    SetActiveWeapon(WeaponSlot.Secondary);
                    break;
                case 2:
                    SetActiveWeapon(WeaponSlot.Grenade);
                    break;
                case 3:
                    SetActiveWeapon(WeaponSlot.Tool);
                    break;    
                default:
                    SetActiveWeapon(WeaponSlot.Primary);
                    rigController.Play("Unarmed");
                    break;
            }
        }

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

    public IEnumerator putAllweaponAway()
    {
        isHolstered = true;
        var weapon = GetWeapon(activeweaponIndex);
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

    public IEnumerator HolsterWeapon(int index)
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
