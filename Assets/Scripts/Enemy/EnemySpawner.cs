using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configurações de Spawn")]
    public GameObject enemyPrefab;
    public int baseEnemiesPerWave = 5;
    public float timeBetweenWaves = 5f;
    public Transform[] spawnPoints;

    [Header("Escalonamento de Dificuldade")]
    public float healthMultiplier = 1.2f;
    public float speedMultiplier = 1.1f;

    [Header("UI")]
    public Text waveText;
    public Text countdownText;

    private int waveNumber = 0;
    private int enemiesAlive = 0;
    private bool waveInProgress = false;

    [Header("Iluminação dinâmica")]
    public Transform directionalLight; // arraste sua luz aqui
    public float lightRotationStep = 10f; // graus a rotacionar por wave
    public float rotationDuration = 2f;   // tempo da transição suave


    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    void Update()
    {
        if (enemiesAlive == 0 && !waveInProgress)
        {
            StartCoroutine(StartNextWave());
        }
    }

    IEnumerator StartNextWave()
    {
        waveInProgress = true;
        waveNumber++;

        if (waveText != null)
            waveText.text = $"Wave {waveNumber}";

        if (directionalLight != null)
        {
            StartCoroutine(RotateLight(directionalLight, lightRotationStep, rotationDuration));
        }

        float countdown = timeBetweenWaves;
        while (countdown > 0)
        {
            if (countdownText != null)
                countdownText.text = $"Próxima wave em {countdown:F1}s";
            countdown -= Time.deltaTime;
            yield return null;
        }

        if (countdownText != null)
            countdownText.text = "";

        int enemiesToSpawn = baseEnemiesPerWave + (waveNumber * 2);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
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

            // Escala saúde e velocidade do inimigo conforme wave
            float scaledHealth = enemyScript.baseHealth * Mathf.Pow(healthMultiplier, waveNumber - 1);
            float scaledSpeed = enemyScript.baseSpeed * Mathf.Pow(speedMultiplier, waveNumber - 1);

        enemyScript.Initialize((int)scaledHealth, scaledSpeed);
        }
        else
        {
            Debug.LogError("Enemy prefab está sem o script Enemy!");
        }
    }
    IEnumerator RotateLight(Transform light, float angle, float duration)
    {
        Quaternion initialRotation = light.rotation;
        Quaternion finalRotation = initialRotation * Quaternion.Euler(angle, 0f, 0f);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            light.rotation = Quaternion.Slerp(initialRotation, finalRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        light.rotation = finalRotation;
    }

}
