using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configurações de Spawn")]
    public GameObject enemyPrefab;
    public int enemiesPerWave = 5;
    public float timeBetweenWaves = 5f;
    public Transform[] spawnPoints;

    private int waveNumber = 0;
    private int enemiesAlive = 0;
    private bool waveInProgress = false;

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    void Update()
    {
        // Aguarda fim da wave para começar outra
        if (enemiesAlive == 0 && !waveInProgress)
        {
            StartCoroutine(StartNextWave());
        }
    }

    IEnumerator StartNextWave()
    {
        waveInProgress = true;
        waveNumber++;
        yield return new WaitForSeconds(timeBetweenWaves);

        int enemiesToSpawn = enemiesPerWave + waveNumber;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f); // espaçamento entre inimigos
        }

        waveInProgress = false;
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab não está atribuído!");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemiesAlive++;
            enemyScript.OnDeath += () => enemiesAlive--;
        }
        else
        {
            Debug.LogError("Enemy prefab está sem o script Enemy!");
        }
    }
}
