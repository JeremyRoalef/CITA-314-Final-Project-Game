using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField]
    WeaponSO weaponSO;

    const string PLAYER_STRING = "Player";

    protected override void OnPickup(ToggleWeaponSwapping activeWeapon)
    {
        if (activeWeapon != null)
        {
            activeWeapon.AddNewWeapon(weaponSO);
        }
    }
}
