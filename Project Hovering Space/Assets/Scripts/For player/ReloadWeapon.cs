using UnityEngine;
using UnityEngine.InputSystem;

public class ReloadWeapon : MonoBehaviour
{
    [Header("Properties")]
    public Animator rigController;
    public WeaponAnimationEvents animationEvents;
    public ActiveWeapon activeWeapon;
    public Transform leftHand;

    GameObject magazineHand;
    // Start is called before the first frame update
    void Start()
    {
        animationEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
    }

    // Update is called once per frame

    public void Reload(InputAction.CallbackContext value)
    {
        RaycastGrenade nade = activeWeapon.GetActiveNade();
        if (nade)
            rigController.SetInteger("NumOfNade", nade.AvailableQuantity);


        RaycastBulletGun weapon = activeWeapon.GetActiveGun();
        RaycastNadeLauncher nadeLauncher = activeWeapon.GetActiveGrenadeLauncher();
        if (weapon)
        {
            //if (Input.GetKeyDown(KeyCode.R) || weapon.ammoCount <= 0)
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (!activeWeapon.isHolstered)
                {
                    rigController.SetTrigger("reload_weapon");
                    weapon.SetReloading(true);
                }
            }
            if (weapon.isFiring)
            {
                //update UI ammo count
            }
        }
        if (nadeLauncher)
        {
            //if (Input.GetKeyDown(KeyCode.R) || weapon.ammoCount <= 0)
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (!activeWeapon.isHolstered)
                {
                    rigController.SetTrigger("reload_weapon");
                    nadeLauncher.SetReloading(true);
                }
            }
            if (nadeLauncher.isFiring)
            {
                //update UI ammo count
            }
        }
    }

    void OnAnimationEvent(string eventName)
    {
        switch (eventName)
        {
            case "detach_magazine":
                DettachMagazine();
                break;
            case "drop_magazine":
                DropMagazine();
                break;
            case "refill_magazine":
                RefillMagazine();
                break;
            case "attach_magazine":
                AttachMagazine();
                break;
            case "endOf_Reload":
                EndReload();
                break;
            case "detach_magazine_gl":
                DettachMagazine_GL();
                break;
            case "attach_magazine_gl":
                AttachMagazine_GL();
                break;
            case "endOf_Reload_gl":
                EndReload_GL();
                break;
            case "throw_nade":
                NadeleaveHand();
                break;
            case "RestockNade":
                RestockNade();
                break;
        }
    }

    void DettachMagazine()
    {
        RaycastBulletGun weapon = activeWeapon.GetActiveGun();
        magazineHand = Instantiate(weapon.magazine, leftHand, true);
        weapon.magazine.SetActive(false);
    }
    void DropMagazine()
    {
        GameObject droppedMagazine = Instantiate(magazineHand, magazineHand.transform.position, magazineHand.transform.rotation);
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.GetComponent<Rigidbody>().useGravity = false;
        droppedMagazine.GetComponent<Rigidbody>().AddForce(-6f, -2f, 4f);
        droppedMagazine.AddComponent<BoxCollider>();
        Destroy(droppedMagazine, 5);
        magazineHand.SetActive(false);
    }
    void RefillMagazine()
    {
        magazineHand.SetActive(true);
    }
    void AttachMagazine()
    {
        RaycastBulletGun weapon = activeWeapon.GetActiveGun();
        weapon.magazine.SetActive(true);
        Destroy(magazineHand);
        weapon.ammoCount = weapon.clipSize;
        rigController.ResetTrigger("reload_weapon");
        //update UI ammo count
    }
    void EndReload()
    {
        RaycastBulletGun weapon = activeWeapon.GetActiveGun();
        weapon.SetReloading(false);
        //update UI ammo count
    }
    void DettachMagazine_GL()
    {
        RaycastNadeLauncher weapon = activeWeapon.GetActiveGrenadeLauncher();
        magazineHand = Instantiate(weapon.magazine, leftHand, true);
        weapon.magazine.SetActive(false);
    }
    void AttachMagazine_GL()
    {
        RaycastNadeLauncher weapon = activeWeapon.GetActiveGrenadeLauncher();
        weapon.magazine.SetActive(true);
        Destroy(magazineHand);
        weapon.ammoCount = weapon.clipSize;
        rigController.ResetTrigger("reload_weapon");
        //update UI ammo count
    }
    void EndReload_GL()
    {
        RaycastNadeLauncher weapon = activeWeapon.GetActiveGrenadeLauncher();
        weapon.SetReloading(false);
        //update UI ammo count
    }
    void NadeleaveHand()
    {
        RaycastGrenade nade = activeWeapon.GetActiveNade();
        GameObject ThrowingNade = Instantiate(nade.ThrowNade, nade.transform.position, nade.transform.rotation);
        Vector3 direction = (nade.raycastTarget.position - nade.transform.position).normalized;

        ThrowingNade.GetComponent<Rigidbody>().AddForce(direction * nade.ThrowForce, ForceMode.VelocityChange);

        nade.HandNade.SetActive(false);
        nade.AvailableQuantity--;
        rigController.SetInteger("NumOfNade", nade.AvailableQuantity);
        rigController.ResetTrigger("throw_nade");
    }
    void RestockNade()
    {
        RaycastGrenade nade = activeWeapon.GetActiveNade();
        nade.HandNade.SetActive(true);
    }
}
