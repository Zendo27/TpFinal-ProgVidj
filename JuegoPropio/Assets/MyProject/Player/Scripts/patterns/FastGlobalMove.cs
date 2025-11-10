using UnityEngine;

// Estrategia concreta: movimiento brusco en coordenadas globales
public class FastGlobalMove : IMoveStrategy
{
    public void Move(Transform transform, float speed)
    {
        // Leer input de teclas sin suavizado (inmediato)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if (direction.magnitude > 1f)
            direction.Normalize();

        // Movimiento directo y rápido en espacio global
        transform.Translate(direction * (speed * 2) * Time.deltaTime, Space.World);
    }
}
