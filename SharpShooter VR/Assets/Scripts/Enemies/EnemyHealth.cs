using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    int startingHealth = 3;

    [SerializeField]
    int currentHealth;

    [SerializeField]
    GameObject deathVFX;

    GameManager gameManager;

    private void Awake()
    {
        currentHealth = startingHealth;
    }
    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.AdjustEnemiesLeft(1);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Temporary
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        gameManager.AdjustEnemiesLeft(-1);


        Destroy(gameObject);
    }
}
