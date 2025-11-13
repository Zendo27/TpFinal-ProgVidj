using UnityEngine;

/// <summary>
/// Controlador general del enemigo: puede patrullar, perseguir y opcionalmente atacar.
/// </summary>
public class EnemyController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float detectionRange = 10f;

    [Header("Ataque (opcional)")]
    [SerializeField] private float attackRange = 6f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private IMoveEnemyStrategy currentStrategy;
    private PatrolEnemyMove patrolStrategy;
    private IMoveEnemyStrategy chaseStrategy;
    private IAttackStrategy attackStrategy;
    private DiagonalBounceMove diagonalStrategy;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player")?.transform;

        // Estrategias de movimiento
        patrolStrategy = new PatrolEnemyMove(transform.position);
        chaseStrategy = new ChaseEnemyMove();

        // Estrategia inicial
        currentStrategy = patrolStrategy;

        // Si tiene bulletPrefab configurado => puede atacar
        if (bulletPrefab != null)
        {
            attackStrategy = new RangedAttack(bulletPrefab, transform);
        }
        else
        {
            Debug.Log($"{name} no tiene configurado ataque (sin bulletPrefab).");
        }

        diagonalStrategy = new DiagonalBounceMove();

        // Si este enemigo es el Enemy03, usa el movimiento diagonal
        if (name.Contains("Enemy03"))
        {
            currentStrategy = diagonalStrategy;
        }

    }

    private void Update()
    {
        if (playerTransform == null) return;

        // 👇 Si este enemigo es Enemy03, solo usa su movimiento diagonal y no cambia estrategia
        if (name.Contains("Enemy03"))
        {
            currentStrategy?.Move(transform, playerTransform, speed);
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // 🔄 Cambiar entre estrategias de movimiento
        if (distanceToPlayer <= detectionRange && currentStrategy != chaseStrategy)
            currentStrategy = chaseStrategy;
        else if (distanceToPlayer > detectionRange && currentStrategy != patrolStrategy)
            currentStrategy = patrolStrategy;

        // 🧭 Ejecutar movimiento
        currentStrategy?.Move(transform, playerTransform, speed);

        // 💥 Ejecutar ataque solo si existe y el jugador está en rango
        if (attackStrategy != null && distanceToPlayer <= attackRange)
        {
            try
            {
                attackStrategy.Attack(transform, playerTransform);
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning($"Error en ataque de {name}: {ex.Message}");
            }
        }
    }


    // 🔄 Gira al colisionar con paredes u obstáculos (pero no con el suelo)
    private void OnCollisionEnter(Collision collision)
    {
        if (currentStrategy == patrolStrategy && !collision.gameObject.CompareTag("Ground"))
        {
            patrolStrategy.OnCollisionWithWall(transform);
        }
        else if (currentStrategy == diagonalStrategy)
        {
            diagonalStrategy.OnCollisionWithWall(transform, collision);
        }
    }
}
