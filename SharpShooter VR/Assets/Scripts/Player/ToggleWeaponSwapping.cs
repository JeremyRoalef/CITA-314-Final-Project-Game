using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleWeaponSwapping : MonoBehaviour
{
    [Header("Input Action References")]

    [SerializeField]
    InputActionReference togglePreviousWeaponInput;

    [SerializeField]
    InputActionReference toggleNextWeaponInput;



    [Header("Locomotion System References")]

    [SerializeField]
    ActionBasedContinuousTurnProvider turnProvider;

    [SerializeField]
    TwoHandedGrabMoveProvider grabMoveProvider;

    [SerializeField]
    [Tooltip("The objects responsible for ray interactions on the active weapon")]
    GameObject[] rayControllers;


    [Header("Weapon Info")]

    [SerializeField]
    List <WeaponSO> weaponsPickedUp = new List<WeaponSO>();

    [SerializeField]
    ActiveWeapon activeWeapon;

    int activeWeaponIndex;

    void Start()
    {
        weaponsPickedUp.Add(null); //This will be to toggle locomotions on/off if current weapon is null
        activeWeaponIndex = 0;
    }

    private void Update()
    {
        if (togglePreviousWeaponInput.action.triggered)
        {
            TogglePreviousWeapon();
        }
        if (toggleNextWeaponInput.action.triggered)
        {
            ToggleNextWeapon();
        }
    }

    public void TogglePreviousWeapon()
    {
        Debug.Log("Toggling previous weapon...");

        //Update weapon index
        if (activeWeaponIndex == 0)
        {
            activeWeaponIndex = weaponsPickedUp.Count - 1;
        }
        else
        {
            activeWeaponIndex--;
        }

        //Locomotion Logic
        if (weaponsPickedUp[activeWeaponIndex] != null)
        {
            EnableLocomotion(false);
        }
        else
        {
            EnableLocomotion(true);
        }

        //Weapon Swap
        Debug.Log("New Index: " +  activeWeaponIndex.ToString());
        SwitchToNewWeapon();
    }

    public void ToggleNextWeapon()
    {
        Debug.Log("Toggling next weapon");

        //Update weapon index
        if (activeWeaponIndex == weaponsPickedUp.Count - 1)
        {
            activeWeaponIndex = 0;
        }
        else
        {
            activeWeaponIndex++;
        }

        //Locomotion Logic
        if (weaponsPickedUp[activeWeaponIndex] != null)
        {
            EnableLocomotion(false);
        }
        else
        {
            EnableLocomotion(true);
        }

        //Weapon Swap
        Debug.Log("New Index: " + activeWeaponIndex.ToString());
        SwitchToNewWeapon();
    }

    private void EnableLocomotion(bool isActive)
    {
        turnProvider.enabled = isActive;
        grabMoveProvider.enabled = isActive;
        foreach (GameObject rayController in rayControllers)
        {
            rayController.SetActive(isActive);
        }
    }

    private void SwitchToNewWeapon()
    {
        activeWeapon.SwitchWeapon(weaponsPickedUp[activeWeaponIndex]);
    }

    public void AddNewWeapon(WeaponSO newWeapon)
    {
        //Find weapon in inventory
        foreach (WeaponSO weaponSO in weaponsPickedUp)
        {
            if (newWeapon == weaponSO)
            {
                Debug.Log("Weapon already picked up");
                //Weapon exists in inventory
                return;
            }
        }

        //WeaponSO not already picked up
        weaponsPickedUp.Add(newWeapon);
    }

    internal void AddAmmoForWeapon(WeaponSO weaponSO, int ammoCount)
    {
        activeWeapon.AdjustAmmoForWeapon(weaponSO, ammoCount);
    }
}
