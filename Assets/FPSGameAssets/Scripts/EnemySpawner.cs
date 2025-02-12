using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //this will be a reference to the Dementor pre-fab in the Assets folder, NOT the hierarchy
    public GameObject enemyPrefab;

    public int spawnFrequency = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!enemyPrefab)
            return;
        
        InvokeRepeating("SpawnEnemy", 2, spawnFrequency);
    }

    void SpawnEnemy()
    {
        var positionOffset = Random.insideUnitSphere * 20;

        Instantiate(enemyPrefab, transform.position + positionOffset, transform.rotation);
    }
}
