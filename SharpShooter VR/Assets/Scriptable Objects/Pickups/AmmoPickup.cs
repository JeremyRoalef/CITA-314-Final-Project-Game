using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField]
    [Tooltip("Add weapon types that will receive the ammo here")]
    WeaponSO[] ammoForGivenWeaponSO;

    [SerializeField]
    int ammoCount = 24;

    protected override void OnPickup(ToggleWeaponSwapping weaponSwap)
    {
        foreach (WeaponSO weaponSO in ammoForGivenWeaponSO)
        {
            weaponSwap.AddAmmoForWeapon(weaponSO, ammoCount);
        }
    }
}
