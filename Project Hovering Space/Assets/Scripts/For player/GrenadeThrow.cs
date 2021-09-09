using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    [HideInInspector] public Animator rigController;
    public WeaponAnimationEvents animationEvents;
    public ActiveWeapon activeWeapon;
    // Start is called before the first frame update
    private void Start()
    {
        animationEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
    }
    private void Update()
    {
        RaycastGrenade nade = (RaycastGrenade)activeWeapon.GetActiveGun();
        rigController.SetInteger("NumOfNade", nade.AvailableQuantity);
    }
    void OnAnimationEvent(string eventName)
    {
        switch (eventName)
        {
            case "throw_nade":
                NadeleaveHand();
                break;
        }
    }
    void NadeleaveHand()
    {
        RaycastGrenade nade = (RaycastGrenade)activeWeapon.GetActiveGun();
        nade.AvailableQuantity--;
        rigController.SetInteger("NumOfNade", nade.AvailableQuantity);
        Debug.Log("Nade left hand");
        rigController.ResetTrigger("throw_nade");
    }
}
