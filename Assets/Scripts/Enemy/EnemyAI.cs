using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    private Animator anim;

    private bool isDead = false;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead || agent == null || !agent.isOnNavMesh)
            return;

        if (target != null)
        {
            agent.SetDestination(target.position);

            // Atualiza o parâmetro de andar com base na velocidade
            if (anim != null)
                anim.SetBool("isWalking", agent.velocity.magnitude > 0.1f);
        }
    }

    // Pode ser chamado por outro script (como Enemy.cs) ao morrer
    public void MarkAsDead()
    {
        isDead = true;

        if (agent != null)
            agent.enabled = false;

        if (anim != null)
            anim.SetBool("isDead", true);

        Destroy(this); // remove o script após a execução (opcional)
    }
}
