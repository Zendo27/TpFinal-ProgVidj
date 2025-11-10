using UnityEngine;

// Estrategia concreta: movimiento suave en coordenadas globales
public class SmoothGlobalMove : IMoveStrategy
{
    public void Move(Transform transform, float speed)
    {
        // Leer input de teclas WASD o flechas (suavizado)
        float horizontal = Input.GetAxis("Horizontal"); // A/D o ←/→
        float vertical = Input.GetAxis("Vertical");     // W/S o ↑/↓

        // Dirección global en plano XZ
        Vector3 direction = new Vector3(horizontal, 0, vertical);

        // Normalizamos para que en diagonal no sea más rápido
        if (direction.magnitude > 1f)
            direction.Normalize();

        // Usamos Translate en espacio global
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}
