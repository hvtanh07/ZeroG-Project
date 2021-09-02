using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadWeapon : MonoBehaviour
{
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
    void Update()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        if (weapon)
        {
            if (Input.GetKeyDown(KeyCode.R) || weapon.ammoCount <= 0)
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
            case "detach_magazinefromGun":
                DettachMagazineandDrop();
                break;
            case "Get_pos":
                SetPosforMagonHand();
                break;
        }
    }

    void DettachMagazine()
    {
        //create a mag on player hand
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        magazineHand = Instantiate(weapon.magazine, leftHand, true);
        weapon.magazine.SetActive(false);
    }
    void SetPosforMagonHand()//replace dettach mag & drop mag
    {
        //create Magaznie on player hand
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        magazineHand = Instantiate(weapon.magazine, leftHand, true);
        magazineHand.SetActive(false);      
    }
    void DettachMagazineandDrop()//replace dettach mag & drop mag
    {
        //create Magaznie on player hand
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        //create fake mag to drop out
        GameObject droppedMagazine = Instantiate(weapon.magazine, weapon.magazine.transform.position, weapon.magazine.transform.rotation);
        weapon.magazine.SetActive(false);
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.GetComponent<Rigidbody>().useGravity = false;
        droppedMagazine.AddComponent<BoxCollider>();

        Destroy(droppedMagazine, 5);
        //magazineHand.SetActive(false);
    }
    void DropMagazine()
    {
        GameObject droppedMagazine = Instantiate(magazineHand, magazineHand.transform.position, magazineHand.transform.rotation);
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.GetComponent<Rigidbody>().useGravity = false;
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
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        weapon.magazine.SetActive(true);
        Destroy(magazineHand);
        weapon.ammoCount = weapon.clipSize;
        rigController.ResetTrigger("reload_weapon");
        //update UI ammo count
    }
    void EndReload()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        weapon.SetReloading(false);
        //update UI ammo count
    }
}
