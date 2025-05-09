using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleFXCleanup : MonoBehaviour
{
    ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

        Invoke("DestroyParticleSystem", particleSystem.main.duration);

    }

    void DestroyParticleSystem()
    {
        Destroy(gameObject);
    }

}
