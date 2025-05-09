using System;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    AudioSource playerHealthSource;

    [SerializeField]
    AudioClip playerHitClip;

    [SerializeField]
    AudioClip playerDeathClip;

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
    bool isDead = false;

    private void Awake()
    {
        currentHealth = startingHealth;
        AdjustShieldUI();
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;
        playerHealthSource.clip = playerHitClip;
        playerHealthSource.Play();

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
        if (isDead) return;

        isDead = true;

        playerHealthSource.clip = playerDeathClip;
        playerHealthSource.Play();

        Invoke("LoadMainMenuScene", 3f);
    }

    void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
