using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //this will be a reference to the Dementor pre-fab in the Assets folder, NOT the hierarchy
    public GameObject enemyPrefab;
    public int spawnFrequency = 5;
    public int maxEnemyCount = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!enemyPrefab)
            return;
        
        // InvokeRepeating("SpawnEnemy", 2, spawnFrequency);
        Debug.Log("Coroutine started --------- " + Time.time);
        StartCoroutine(SpawnEnemies(spawnFrequency));
    }

    void SpawnEnemy()
    {
        var positionOffset = Random.insideUnitSphere * 20;

        Instantiate(enemyPrefab, transform.position + positionOffset, transform.rotation);
    }

    IEnumerator SpawnEnemies(float spawnInterval)
    {
        Debug.Log("Before yield:" + Time.time);

        while (true) {

            int enemyCount = GameObject.FindGameObjectsWithTag("Dementor").Length;

            //check to see if we've reached the enemy count limit
            if (enemyCount < maxEnemyCount) {
                SpawnEnemy();
            }
            
            yield return new WaitForSeconds(spawnInterval);
            Debug.Log("After yield: " + Time.time);
        }
    }
}
