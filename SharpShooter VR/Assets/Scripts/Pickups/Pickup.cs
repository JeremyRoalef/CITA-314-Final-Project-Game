using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class Pickup : XRGrabInteractable
{
    [SerializeField]
    float rotationSpeed = 100f;

    //private void Update()
    //{
    //    transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    //}

    const string PLAYER_STRING = "Player";

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(PLAYER_STRING))
    //    {
    //        ActiveWeapon weaponSwaps = other.GetComponentInChildren<ActiveWeapon>();
    //        OnPickup(weaponSwaps);
    //        Destroy(gameObject);
    //    }
    //}

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        ToggleWeaponSwapping[] weaponSwaps = FindObjectsByType<ToggleWeaponSwapping>(FindObjectsSortMode.None);
        foreach(ToggleWeaponSwapping weaponSwap in weaponSwaps)
        {
            OnPickup(weaponSwap);
        }

        Destroy(gameObject);
    }

    protected abstract void OnPickup(ToggleWeaponSwapping weaponSwap);
}
