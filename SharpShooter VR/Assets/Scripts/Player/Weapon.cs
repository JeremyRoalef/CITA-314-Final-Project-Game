using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    ParticleSystem muzzleFlash;

    [SerializeField]
    LayerMask interactionLayers;

    //RaycastHit interacts with rigidbodys and colliders
    RaycastHit hit;
    CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shoot(WeaponSO weaponSO)
    {
        muzzleFlash.Play();
        impulseSource.GenerateImpulse();

        //Last argument ignores triggers
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, 
            out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {
            //If hit has output, this will run
            Debug.Log(hit.collider.name);

            EnemyHealth enemyHealth = hit.transform.GetComponentInParent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage); //Enemy health null? (same as below)

            GameObject hitVFX = Instantiate(weaponSO.hitVFXPrefab, hit.point, Quaternion.identity); //hit.point will return the location the ray hit the collider 

            /*
            if (hit.transform.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                Debug.Log("Hit enemy");
                enemyHealth.TakeDamage(damageAmount);
            }
            */
        }
    }
}
