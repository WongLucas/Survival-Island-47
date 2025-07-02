using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configurações de Spawn")]
    public GameObject enemyPrefab;
    public int baseEnemiesPerWave = 5;
    public float timeBetweenWaves = 5f;

    [Header("Spawn Dinâmico")]
    public Transform playerTransform;
    public float spawnRadius = 20f;
    public LayerMask groundLayer;

    [Header("Escalonamento de Dificuldade")]
    public float healthMultiplier = 1.2f;
    public float speedMultiplier = 1.1f;

    [Header("UI")]
    public Text waveText;
    public Text countdownText;

    private int waveNumber = 0;
    private int enemiesAlive = 0;
    private bool waveInProgress = false;
    private bool canSkipWave = false;
    private bool skipRequested = false;

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

        if (canSkipWave && Input.GetKeyDown(KeyCode.N))
        {
            skipRequested = true;
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
        canSkipWave = true;
        skipRequested = false;

        while (countdown > 0f)
        {
            if (skipRequested)
            {
                if (countdownText != null)
                    countdownText.text = "SOBREVIVA!";
                    yield return new WaitForSeconds(2f); // mantém o texto por 2 segundos
                    countdownText.text = "";
                break;
            }

            if (countdownText != null)
            {
                countdownText.text = $"Próxima wave em {countdown:F1}s";
            }

            countdown -= Time.deltaTime;
            yield return null;
        }

        canSkipWave = false;
        skipRequested = false;

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

        Vector3 spawnPos = GetSpawnPositionAroundPlayer();
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

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
    Vector3 GetSpawnPositionAroundPlayer()
    {
        for (int attempt = 0; attempt < 10; attempt++)
        {
            Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 candidatePos = playerTransform.position + new Vector3(randomCircle.x, 10f, randomCircle.y);

            // Faz um Raycast para baixo até encontrar o terreno
            if (Physics.Raycast(candidatePos, Vector3.down, out RaycastHit hit, 20f, groundLayer))
            {
                float slope = Vector3.Angle(hit.normal, Vector3.up);
                if (slope < 50f)
                {
                return hit.point;                    
                }
            }
        }

        Debug.LogWarning("Falha ao encontrar uma posição de spawn válida.");
        return playerTransform.position + Vector3.up * 2f; // fallback
    }


}
