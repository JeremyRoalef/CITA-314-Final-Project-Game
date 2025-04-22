using System;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    int startingHealth = 5;

    [SerializeField]
    int currentHealth;

    [SerializeField]
    CinemachineVirtualCamera deathVirtualCamera;
    
    [SerializeField]
    Transform weaponCamera;

    [SerializeField]
    Image[] shieldBars;

    [SerializeField]
    GameObject gameOverContainer;

    int gameOverVirtualCameraPriority = 20;

    private void Awake()
    {
        currentHealth = startingHealth;
        AdjustShieldUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        AdjustShieldUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void AdjustShieldUI()
    {
        for (int i = 0; i < shieldBars.Length; i++)
        {
            if (i < currentHealth)
            {
                shieldBars[i].gameObject.SetActive(true);
            }
            else
            {
                shieldBars[i].gameObject.SetActive(false);
            }
        }
    }

    private void Die()
    {
        weaponCamera.parent = null;
        deathVirtualCamera.Priority = gameOverVirtualCameraPriority;
        gameOverContainer.SetActive(true);
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.SetCursorState(false);

        Destroy(gameObject);
    }
}
