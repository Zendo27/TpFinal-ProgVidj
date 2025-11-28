using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    [SerializeField] private float chaseSpeed = 3.5f;        // Velocidad del agente
    [SerializeField] private float chaseRange = 15f;         // Distancia para comenzar a perseguir
    [SerializeField] private float stopDistance = 2f;        // Distancia mínima al jugador

    [Header("Ataque")]
    [SerializeField] private float attackCooldown = 1f;      // Tiempo entre ataques
    [SerializeField] private int damageToPlayer = 1;         // Daño que hace

    private NavMeshAgent agent;
    private Transform player;
    private float lastAttackTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("El enemigo necesita un componente NavMeshAgent.");
            enabled = false;
            return;
        }

        // Ajustamos velocidad modificable desde Inspector
        agent.speed = chaseSpeed;
        agent.stoppingDistance = stopDistance;

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
            Debug.LogWarning("EnemyNavMesh no encontró al jugador.");
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Perseguir al jugador si está en rango
        if (distance <= chaseRange)
        {
            agent.SetDestination(player.position);

            // Intentar atacar si estamos cerca
            TryAttack(distance);
        }
        else
        {
            // Fuera de rango → quedarse quieto
            agent.ResetPath();
        }
    }

    private void TryAttack(float distance)
    {
        if (distance > stopDistance + 0.5f) return;

        // Cooldown del ataque
        if (Time.time - lastAttackTime < attackCooldown) return;

        // Intentar hacer daño al player
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(damageToPlayer);
        }

        lastAttackTime = Time.time;
    }
}
