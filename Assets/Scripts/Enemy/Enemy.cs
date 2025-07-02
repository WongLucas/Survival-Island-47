using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [Header("Status")]
    public float baseSpeed = 3f;
    public int baseHealth = 100;
    private int currentHealth;
    public int rewardMoney = 10;

    [Header("Componentes")]
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator anim;
    private Transform player;

    public Action OnDeath;

    private bool isDead = false;

    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (agent != null && player != null)
        {
            agent.SetDestination(player.position);
            anim?.SetBool("isWalking", true);
        }
    }

    // Chamada após instanciar o inimigo
    public void Initialize(int scaledHealth, float scaledSpeed)
    {
        currentHealth = scaledHealth;

        if (agent != null)
            agent.speed = scaledSpeed;
    }

    void Update()
    {
        if (isDead || player == null || agent == null) return;

        agent.SetDestination(player.position);

        float distance = Vector3.Distance(transform.position, player.position);
        if (anim != null)
        {
            anim.SetBool("isWalking", agent.velocity.magnitude > 0.1f);
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

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
        isDead = true;

        OnDeath?.Invoke();

        if (anim != null)
            anim.SetBool("isDead", true);

        if (agent != null)
            agent.enabled = false;

        // Envia notificação para EnemyAI
        EnemyAI ai = GetComponent<EnemyAI>();
        if (ai != null)
            ai.MarkAsDead();

        Destroy(gameObject, 2f); // tempo para animação
    }

}
