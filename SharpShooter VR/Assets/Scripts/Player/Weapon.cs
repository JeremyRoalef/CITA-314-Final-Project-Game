using Cinemachine;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    AudioSource[] shootSources;

    [SerializeField]
    AudioClip shootAudioClip;

    [SerializeField]
    GameObject shootOrigin;

    [SerializeField]
    ParticleSystem muzzleFlash;

    [SerializeField]
    LayerMask interactionLayers;

    [SerializeField]
    GameObject aimTargetObject;
    
    [SerializeField]
    TextMeshPro ammoText;

    //RaycastHit interacts with rigidbodys and colliders
    RaycastHit hit;
    CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        aimTargetObject.SetActive(false);
        impulseSource = GetComponent<CinemachineImpulseSource>();
        foreach (AudioSource source in shootSources)
        {
            source.clip = shootAudioClip;
        }
    }

    private void Update()
    {
        //Laser sight on weapon
        if (Physics.Raycast(shootOrigin.transform.position, shootOrigin.transform.forward,
            out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {
            //If hit has output, this will run
            //Debug.Log(hit.collider.name);

            aimTargetObject.SetActive(true);
            aimTargetObject.transform.position = hit.point;
        }
    }

    public void Shoot(WeaponSO weaponSO)
    {
        muzzleFlash.Play();
        impulseSource.GenerateImpulse();

        PlayShootSound();

        //Last argument ignores triggers
        if (Physics.Raycast(shootOrigin.transform.position, shootOrigin.transform.forward,
            out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {
            //If hit has output, this will run
            //Debug.Log(hit.collider.name);

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

    private void PlayShootSound()
    {
        if (shootSources == null) return;
        if (shootAudioClip == null) return;

        foreach (AudioSource source in shootSources){
            if (source.isPlaying) { continue; }
            else
            {
                source.Play();
                break;
            }
        }
    }

    public void UpdateAmmoText(string newText)
    {
        ammoText.text = newText;
    }
}
