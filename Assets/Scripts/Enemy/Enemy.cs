using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public Transform target;
    private NavMeshAgent agent;

    public event Action OnDeath;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
