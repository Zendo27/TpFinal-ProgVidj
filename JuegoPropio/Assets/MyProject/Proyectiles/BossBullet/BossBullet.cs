using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 12f;         // Velocidad de la bala
    public int damage = 1;            // Daño que hace al player
    public float lifeTime = 5f;       // Tiempo antes de destruirse

    void Start()
    {
        // Destruye la bala después de X segundos para evitar acumular basura
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Movimiento recto hacia adelante
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si toca al Player
        if (other.CompareTag("Player"))
        {
            // Buscar componente que maneje la vida del jugador
            PlayerHealth player = other.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.TakeDamage(damage);
            }

            // Destruye la bala al impactar
            Destroy(gameObject);
        }

        // Si toca una pared o cualquier cosa que no sea el boss, también se destruye
        if (!other.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
