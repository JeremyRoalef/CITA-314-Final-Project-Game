using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField]
    int ammoAmount = 24;

    protected override void OnPickup(ToggleWeaponSwapping activeWeapon)
    {
        //activeWeapon.AdjustAmmo(ammoAmount);
    }
}
