using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] waypoints;
    public float spawnRate = 2f;
    public float timeBetweenWaves = 5f;
    public int enemiesPerWave = 10;

    // Initial delay
    private float timer = 0.75f;
    private int waveNumber = 0;
    private int enemiesSpawnedInWave = 0;

    private void Start()
    {

    }

    void Update()
    {
        if (enemiesSpawnedInWave >= enemiesPerWave)
        {
            // Simple wave complete logic - wait then start next wave
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StartNextWave();
            }
            return;
        }


        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = 1f / spawnRate;
        }

        timer -= Time.deltaTime;
    }

    void StartNextWave()
    {
        waveNumber++;
        enemiesSpawnedInWave = 0;
        timer = timeBetweenWaves;
        Debug.Log("Starting Wave: " + waveNumber);
    }

    void SpawnEnemy()
    {
        GameObject enemyGO = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        EnemyMovement enemyScript = enemyGO.GetComponent<EnemyMovement>();

        enemyScript.SetWaypoints(waypoints);
        enemiesSpawnedInWave++;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 1; i < waypoints.Length; i++)
        {
            Gizmos.DrawLine(waypoints[i - 1].position, waypoints[i].position);
        }

    }
}