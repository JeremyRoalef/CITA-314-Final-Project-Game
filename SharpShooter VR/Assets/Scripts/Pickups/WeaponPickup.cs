using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField]
    WeaponSO weaponSO;

    const string PLAYER_STRING = "Player";

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        if (activeWeapon != null)
        {
            activeWeapon.SwitchWeapon(weaponSO);
        }
    }
}
