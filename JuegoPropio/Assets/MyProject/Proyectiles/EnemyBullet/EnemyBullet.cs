using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float homingDuration = 1f;
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private int damage = 1; // <<< DAÑO AGREGADO

    private Vector3 direction;
    private Transform target;
    private float timer;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
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

        if (target != null && timer <= homingDuration)
        {
            Vector3 toTarget = (target.position - transform.position).normalized;
            direction = Vector3.Lerp(direction, toTarget, 0.1f);
        }

        transform.position += direction * speed * Time.deltaTime;

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
        // <<< DAÑO AGREGADO >>>
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<PlayerHealth>(); // Ajustá si tu script tiene otro nombre
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
