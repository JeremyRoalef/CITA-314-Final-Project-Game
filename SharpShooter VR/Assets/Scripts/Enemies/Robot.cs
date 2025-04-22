using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    NavMeshAgent agent;
    CharacterController player;

    const string PLAYER_STRING = "Player";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player ==  null) {return;}
        agent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_STRING))
        {
            GetComponent<EnemyHealth>().Die();
        }
    }
}
