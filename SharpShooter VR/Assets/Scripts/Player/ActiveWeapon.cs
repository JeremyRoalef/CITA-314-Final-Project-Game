using System;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [SerializeField]
    InputActionReference shootInput;

    [SerializeField]
    float maxRayDistance;

    [SerializeField]
    TextMeshProUGUI ammoText;

    [SerializeField]
    GameObject playerHandRenderer;

    [SerializeField]
    FirstPersonController firstPersonController;

    //Dictionary for holding unique weapons & their respective ammo count
    Dictionary<WeaponSO, int> weaponAmmoCounts = new Dictionary<WeaponSO, int>();
    Dictionary<WeaponSO, GameObject> weaponPrefabs = new Dictionary<WeaponSO, GameObject>();


    Animator animator;
    Weapon currentWeapon;
    WeaponSO currentWeaponSO;
    const string SHOOT_STRING = "Shoot";
    float cameraDefaultZoom;
    float defaultRotationAmount;
    bool canShoot = true;
    bool weaponIsActive = false;
    int currentAmmo;

    private void Awake()
    {
        //starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        //cameraDefaultZoom = playerFollowCamera.m_Lens.FieldOfView;
        //firstPersonController = GetComponentInParent<FirstPersonController>();
        //defaultRotationAmount = firstPersonController.RotationSpeed;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchWeapon(startingWeapon);
        AdjustAmmoForWeapon(currentWeaponSO, currentWeaponSO.MagazineSize);
    }

    // Update is called once per frame
    void Update()
    {
        HandleShoot();
        //HandleZoom();
    }

    //Pass in the change of ammo for ammo count
    public void AdjustAmmoForWeapon(WeaponSO weaponSO, int ammoCount)
    {
        if (weaponAmmoCounts.ContainsKey(weaponSO))
        {
            weaponAmmoCounts[weaponSO] += ammoCount;
            UpdateWeaponAmmoText(weaponSO);
        }
    }

    //public void AdjustAmmo(int amount)
    //{
    //    currentAmmo += amount;
    //    //ammoText.text = currentAmmo.ToString("D3");
    //}

    private void HandleShoot()
    {
        //Conditions to stop
        if (!shootInput.action.IsPressed()) { return; }
        if (!canShoot) { return; }
        if (!weaponIsActive) { return; }
        if (weaponAmmoCounts[currentWeaponSO] <= 0) { return; }

        canShoot = false;
        Invoke("EnableShooting", currentWeaponSO.FireRate);
        AdjustAmmoForWeapon(currentWeaponSO, -1);
        currentWeapon.Shoot(currentWeaponSO);

        //Play the shoot animation, passing the layer and normalized time (0 for each)
        animator.Play(SHOOT_STRING, 0, 0f);

        //if (!currentWeaponSO.IsAutomatic)
        //{
        //    starterAssetsInputs.ShootInput(false);
        //}
    }

    void EnableShooting()
    {
        canShoot = true;
    }

    public void SwitchWeapon(WeaponSO newWeaponSO)
    {
        if (newWeaponSO == null)
        {
            SetActiveWeaponToNone();
            return;
        }

        //Hide Current Weapon
        if (currentWeaponSO != null)
        {
            if (weaponPrefabs.ContainsKey(newWeaponSO))
            {
                weaponPrefabs[currentWeaponSO].SetActive(false);
            }
        }


        if (!weaponPrefabs.ContainsKey(newWeaponSO) && newWeaponSO != null)
        {
            AddNewWeapon(newWeaponSO);
        }

        //Weapon in use
        playerHandRenderer.SetActive(false);
        weaponIsActive = true;
        weaponPrefabs[newWeaponSO].SetActive(true);

        //AdjustAmmo(-currentAmmo);

        //Change weapon
        currentWeaponSO = newWeaponSO;
        Weapon newWeapon = weaponPrefabs[currentWeaponSO].GetComponent<Weapon>();
        currentWeapon = newWeapon;

        UpdateWeaponAmmoText(currentWeaponSO);

        //AdjustAmmoForWeapon(currentWeaponSO, currentWeaponSO.MagazineSize);
    }

    private void SetActiveWeaponToNone()
    {
        //No active weapon
        playerHandRenderer.SetActive(true);
        weaponIsActive = false;
        if (currentWeaponSO != null)
        {
            weaponPrefabs[currentWeaponSO].SetActive(false);
            currentWeaponSO = null;
        }
    }

    private void AddNewWeapon(WeaponSO newWeaponSO)
    {
        GameObject newWeaponObj = Instantiate(newWeaponSO.WeaponPrefab, transform);
        newWeaponObj.SetActive(false);
        weaponPrefabs.Add(newWeaponSO, newWeaponObj);
        weaponAmmoCounts[newWeaponSO] = newWeaponSO.MagazineSize;
    }

    void UpdateWeaponAmmoText(WeaponSO weaponSO)
    {
        string ammoText = $"{weaponAmmoCounts[weaponSO]}";
        weaponPrefabs[weaponSO].GetComponent<Weapon>().UpdateAmmoText(ammoText);
    }

    //void HandleZoom()
    //{
    //    if (!currentWeaponSO.CanZoom) { return; }

    //    if (starterAssetsInputs.zoom)
    //    {
    //        playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
    //        weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
    //        zoomVignette.SetActive(true);
    //        firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotateAmount);
    //    }
    //    else
    //    {
    //        playerFollowCamera.m_Lens.FieldOfView = cameraDefaultZoom;
    //        weaponCamera.fieldOfView = cameraDefaultZoom;
    //        zoomVignette.SetActive(false);
    //        firstPersonController.ChangeRotationSpeed(defaultRotationAmount);
    //    }
    //}
}
