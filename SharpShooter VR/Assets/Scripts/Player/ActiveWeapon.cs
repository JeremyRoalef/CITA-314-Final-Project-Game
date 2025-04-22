using System;
using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField]
    WeaponSO startingWeapon;

    [SerializeField]
    CinemachineVirtualCamera playerFollowCamera;

    [SerializeField]
    Camera weaponCamera; 

    [SerializeField]
    GameObject zoomVignette;

    StarterAssetsInputs starterAssetsInputs;

    [SerializeField]
    float maxRayDistance;

    [SerializeField]
    TextMeshProUGUI ammoText;

    Animator animator;

    Weapon currentWeapon;
    WeaponSO currentWeaponSO;
    FirstPersonController firstPersonController;
    const string SHOOT_STRING = "Shoot";

    float cameraDefaultZoom;
    float defaultRotationAmount;
    bool canShoot = true;
    int currentAmmo;

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        cameraDefaultZoom = playerFollowCamera.m_Lens.FieldOfView;
        firstPersonController = GetComponentInParent<FirstPersonController>();
        defaultRotationAmount = firstPersonController.RotationSpeed;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchWeapon(startingWeapon);
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    // Update is called once per frame
    void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;
        ammoText.text = currentAmmo.ToString("D3");
    }

    private void HandleShoot()
    {
        //Conditions to stop
        if (!starterAssetsInputs.shoot) { return; }
        if (!canShoot) { return; }
        if (currentAmmo <= 0) { return; }

        canShoot = false;
        Invoke("EnableShooting", currentWeaponSO.FireRate);
        AdjustAmmo(-1);
        currentWeapon.Shoot(currentWeaponSO);

        //Play the shoot animation, passing the layer and normalized time (0 for each)
        animator.Play(SHOOT_STRING, 0, 0f);

        if (!currentWeaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }

    void EnableShooting()
    {
        canShoot = true;
    }

    public void SwitchWeapon(WeaponSO newWeaponSO)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        AdjustAmmo(-currentAmmo);

        //Change weapon
        currentWeaponSO = newWeaponSO;
        Weapon newWeapon = Instantiate(currentWeaponSO.WeaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;

        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) { return; }

        if (starterAssetsInputs.zoom)
        {
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
            zoomVignette.SetActive(true);
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotateAmount);
        }
        else
        {
            playerFollowCamera.m_Lens.FieldOfView = cameraDefaultZoom;
            weaponCamera.fieldOfView = cameraDefaultZoom;
            zoomVignette.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationAmount);
        }
    }
}
