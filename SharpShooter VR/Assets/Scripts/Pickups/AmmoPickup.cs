using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField]
    int ammoAmount = 24;

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.AdjustAmmo(ammoAmount);
    }
}
