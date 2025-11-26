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
        // Si tiene vida, le hacemos daño
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(1);
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
        // Por seguridad, se destruye sola a los 3 segundos si no golpea nada
        Destroy(gameObject, 5f);
    }
}
