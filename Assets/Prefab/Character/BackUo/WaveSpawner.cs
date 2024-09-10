using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private float timeBetweenWaves = 5f; // Time between waves
    [SerializeField] private Transform[] spawnPoints; // Array of spawn points

    public Wave[] waves; // Array of waves
    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    private void Start()
    {
        if (waves.Length > 0)
        {
            StartCoroutine(SpawnWaves());
        }
        else
        {
            Debug.LogError("No waves defined!");
        }
    }

    private IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < waves.Length)
        {
            if (!isSpawning)
            {
                StartCoroutine(SpawnWave(waves[currentWaveIndex]));
                isSpawning = true;
            }

            yield return new WaitForSeconds(timeBetweenWaves);
            currentWaveIndex++;
            isSpawning = false;
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning wave: " + currentWaveIndex);

        foreach (var enemy in wave.enemies)
        {
            if (enemy != null && spawnPoints.Length > 0)
            {
                foreach (var spawnPoint in spawnPoints)
                {
                    Instantiate(enemy, spawnPoint.position, Quaternion.identity);
                    yield return new WaitForSeconds(wave.timeBetweenEnemies); // Delay between each enemy spawn
                }
            }
            else
            {
                Debug.LogError("Enemy or spawnPoints are not properly assigned!");
            }
        }

        // Optionally wait for all enemies to be defeated before proceeding to the next wave
        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return null;
        }
    }

    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemies; // Array of enemy prefabs
        public float timeBetweenEnemies; // Time between spawns of each enemy in the wave
    }
}
