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

    private void OnCollisionEnter(Collision collision)
    {
        // Acá podrías agregar efectos, daño, etc.
        Destroy(gameObject);
    }
}
