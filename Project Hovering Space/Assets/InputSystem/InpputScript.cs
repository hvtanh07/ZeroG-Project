using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InpputScript : MonoBehaviour
{
    public Vector2 inputmovement;
    public void ScrollWheel(InputAction.CallbackContext value)
    {
        float valueScroll = value.ReadValue<float>();        
            if (valueScroll < 0)
                Debug.Log("weapon down");
            if (valueScroll > 0)
                Debug.Log("weapon up");       
    }
    public void WeaponShoot(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Debug.Log("Shoot");
        }
        if (value.canceled)
        {
            Debug.Log("Stop Shooting");
        }
    }
    public void ButtonPressedCheck(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Debug.Log("Button Pressed");
        }      
    }

    public void Aim(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            Debug.Log("Aim");
        }
    }
    public void ScopeAim(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            Debug.Log("Scope aim");
        }
    }

    public void Interacted(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            Debug.Log("Interacted");
        }
    }
    public void Movement(InputAction.CallbackContext value)
    {
        //called once everytime one of WASD pressed
        inputmovement = value.ReadValue<Vector2>();
        Debug.Log(inputmovement.ToString());
    }
}
