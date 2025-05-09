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

    [SerializeField]
    int enemyPool = 1;

    [SerializeField]
    GameObject enemyPoolParentObj;

    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<PlayerHealth>().gameObject;
        StartCoroutine(SpawnEnemy());
        for (int i = 0; i < enemyPool; i++)
        {
            GameObject newEnemy = Instantiate(
                enemyPrefab,
                spawnPoint.position,
                Quaternion.identity);
            newEnemy.transform.parent = enemyPoolParentObj.transform;
            newEnemy.SetActive(false);
        }

    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        //Infinite Looping
        while (true)
        {
            if (player == null) { StopAllCoroutines(); }

            //Enable first inactive enemy
            foreach(Transform enemy in enemyPoolParentObj.transform)
            {
                if (!enemy.gameObject.activeSelf)
                {
                    enemy.transform.position = spawnPoint.position;
                    enemy.gameObject.SetActive(true);
                    break;
                }
            }

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
