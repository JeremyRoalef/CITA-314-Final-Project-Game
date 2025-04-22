using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    float projectileVelocity = 7f;
    [SerializeField]
    int damageAmount = 1;

    [SerializeField]
    GameObject projectilVFX;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void InitializeProjectile(float projectileVelocity, int damageAmount)
    {
        this.projectileVelocity = projectileVelocity;
        this.damageAmount = damageAmount;

        rb.useGravity = false;
        rb.linearVelocity = transform.forward * projectileVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(damageAmount);
        }

        Destroy(gameObject);
        Instantiate(projectilVFX, transform.position, Quaternion.identity);
    }
}
