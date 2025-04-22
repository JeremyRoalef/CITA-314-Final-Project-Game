using System.Collections;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    float delayBetweenSpawning = 5f;

    [SerializeField]
    float initialSpawnDelay = 1f;

    [SerializeField]
    Transform spawnPoint;

    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<PlayerHealth>().gameObject;
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        while (true)
        {
            if (player == null) { StopAllCoroutines(); }

            GameObject newEnemy = Instantiate(
                enemyPrefab,
                spawnPoint.position,
                Quaternion.identity);

            //Delay next spawn
            yield return new WaitForSeconds(delayBetweenSpawning);
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
