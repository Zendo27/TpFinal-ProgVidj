using UnityEngine;

// Clase que maneja el comportamiento de una bala
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // Velocidad de la bala
    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    private void Update()
    {
        // Mueve la bala en la dirección indicada
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Daño a enemigos normales
        if (other.TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage(1);
            Destroy(gameObject);
            return;
        }

        // Daño al Boss
        if (other.TryGetComponent(out BossHealth bossHealth))
        {
            bossHealth.TakeDamage(1);
            Destroy(gameObject);
            return;
        }

        // Si choca con algo que NO es el jugador, se destruye
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Por seguridad, se destruye sola a los 5 segundos si no golpea nada
        Destroy(gameObject, 5f);
    }
}
