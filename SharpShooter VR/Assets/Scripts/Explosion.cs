using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    float radius = 1.5f;

    [SerializeField]
    int damage = 3;

    const string PLAYER_STRING = "Player";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Explode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Explode()
    {
        //Do Damage To Player
        foreach (Collider collider in Physics.OverlapSphere(transform.position, radius))
        {
            if (collider.gameObject.CompareTag(PLAYER_STRING))
            {
                collider.GetComponent<PlayerHealth>().TakeDamage(damage);
                break;
            }
        }
    }
}
