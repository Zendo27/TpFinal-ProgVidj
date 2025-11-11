using UnityEngine;

// Estrategia de movimiento suave en coordenadas globales
// El jugador rota solo sobre el eje Y y al detenerse vuelve a su rotación base
public class SmoothGlobalMove : IMoveStrategy
{
    // Rotación base (cuando está quieto)
    private readonly Vector3 baseRotation = new Vector3(-45f, 180f, 0f);

    public void Move(Transform transform, float speed)
    {
        float horizontal = 0f;
        float vertical = 0f;

        // Movimiento solo con WASD
        if (Input.GetKey(KeyCode.W)) vertical = 1f;
        if (Input.GetKey(KeyCode.S)) vertical = -1f;
        if (Input.GetKey(KeyCode.A)) horizontal = -1f;
        if (Input.GetKey(KeyCode.D)) horizontal = 1f;

        // Dirección en plano XZ (en espacio global)
        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if (direction.magnitude > 1f)
            direction.Normalize();

        // Si hay dirección, rotamos hacia ella
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            Vector3 currentEuler = transform.localEulerAngles;
            Vector3 targetEuler = targetRotation.eulerAngles;

            // Mantiene inclinación fija (X=45, Z=0)
            targetEuler.x = -45f;
            targetEuler.z = 0f;

            // Interpolación suave en Y
            float smoothY = Mathf.LerpAngle(currentEuler.y, targetEuler.y, Time.deltaTime * 10f);
            transform.localEulerAngles = new Vector3(45f, smoothY, 0f);
        }
        else
        {
            // Si no se mueve, volver suavemente a la rotación base
            Vector3 currentEuler = transform.localEulerAngles;

            float smoothY = Mathf.LerpAngle(currentEuler.y, baseRotation.y, Time.deltaTime * 5f);
            float smoothX = Mathf.LerpAngle(currentEuler.x, baseRotation.x, Time.deltaTime * 5f);
            float smoothZ = Mathf.LerpAngle(currentEuler.z, baseRotation.z, Time.deltaTime * 5f);

            transform.localEulerAngles = new Vector3(smoothX, smoothY, smoothZ);
        }

        // Movimiento en espacio global
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}
