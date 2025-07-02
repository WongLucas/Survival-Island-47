using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public float baseSpeed = 3f;
    public int baseHealth = 100;
    private int currentHealth;
    public Action OnDeath;

    private UnityEngine.AI.NavMeshAgent agent;
    private Transform player;
    public int rewardMoney = 10;

    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (agent != null && player != null)
            agent.SetDestination(player.position);
    }

    // Chamada após instanciar o inimigo
    public void Initialize(int scaledHealth, float scaledSpeed)
    {
        currentHealth = scaledHealth;
        if (agent != null)
            agent.speed = scaledSpeed;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= (int)amount;
        if (currentHealth <= 0)
        {
            if (GameManager.Instance != null)
                GameManager.Instance.AddMoney(rewardMoney);

            Die();
        }
    }

    void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}