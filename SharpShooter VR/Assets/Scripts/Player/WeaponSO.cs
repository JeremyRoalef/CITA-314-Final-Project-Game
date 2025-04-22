using StarterAssets;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponSO", fileName = "ScriptableObjects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    [Tooltip("The amount of damage one shot will do")]
    public int Damage = 1;
    [Tooltip("The size of each magazine")]
    public int MagazineSize = 6;
    [Tooltip("The time it takes to reload the weapon")]
    public float ReloadTime = 1f;
    [Tooltip("The delay (in seconds) between each shot fired")]
    public float FireRate = 0.5f;
    [Tooltip("The particles that will play when an object is hit")]
    public GameObject hitVFXPrefab;
    [Tooltip("Enable if the weapon should fire automatically")]
    public bool IsAutomatic = false;
    [Tooltip("The prefab game object of the weapon")]
    public GameObject WeaponPrefab;
    [Tooltip("Enable if the weapon has a zoom in feature")]
    public bool CanZoom = false;
    [Tooltip("How zoomed in the vertical lens FOV will be when zoomed in")]
    public float ZoomAmount = 10f;
    [Tooltip("How fast the player will rotate while zooming")]
    public float ZoomRotateAmount = 0.2f;
}
