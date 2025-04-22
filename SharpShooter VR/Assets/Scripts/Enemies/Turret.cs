using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    Transform turretHead;

    [SerializeField]
    Transform turretProjectileSpawnPoint;

    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    float fireRate = 0.5f;

    [SerializeField]
    int damageAmount;

    [SerializeField]
    float projectileVelocity;

    Transform playerTargetPoint;
    GameObject player;

    const string CINEMACHINE_TARGET_STRING = "CinemachineTarget";
    void Start()
    {
        playerTargetPoint = GameObject.FindGameObjectWithTag(CINEMACHINE_TARGET_STRING).transform;
        player = FindFirstObjectByType<PlayerHealth>().gameObject;
        StartCoroutine(FireWeapon());
    }

    void Update()
    {
        if (playerTargetPoint != null)
        {
            turretHead.LookAt(playerTargetPoint.transform.position);
        }
    }

    IEnumerator FireWeapon()
    {
        while (true)
        {
            if (player == null) { StopAllCoroutines(); }
            GameObject newProjectile = Instantiate(
                projectilePrefab, 
                turretProjectileSpawnPoint.position, 
                Quaternion.identity);
            if (playerTargetPoint != null)
                newProjectile.transform.LookAt(playerTargetPoint.transform.position);

            newProjectile.GetComponent<Projectile>()?.InitializeProjectile(projectileVelocity, damageAmount);

            yield return new WaitForSeconds(fireRate);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
