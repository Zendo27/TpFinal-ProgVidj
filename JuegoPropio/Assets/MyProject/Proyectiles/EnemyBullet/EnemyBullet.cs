using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;           // Velocidad de la bala
    [SerializeField] private float homingDuration = 1f;   // Tiempo que sigue al objetivo
    [SerializeField] private float lifetime = 2f;         // Tiempo antes de destruirse

    private Vector3 direction;
    private Transform target;
    private float timer;

    private void Start()
    {
        // Buscar al jugador automáticamente por Tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
            // Dirección inicial hacia el jugador
            direction = (target.position - transform.position).normalized;
        }
        else
        {
            Debug.LogWarning("EnemyBullet: No se encontró un objeto con Tag 'Player'.");
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Mientras esté en modo persecución (auto-guía)
        if (target != null && timer <= homingDuration)
        {
            // Recalcular dirección hacia el jugador
            Vector3 toTarget = (target.position - transform.position).normalized;
            direction = Vector3.Lerp(direction, toTarget, 0.1f); // suaviza el giro
        }

        // Mover la bala
        transform.position += direction * speed * Time.deltaTime;

        // Destruir tras cierto tiempo
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }


    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
