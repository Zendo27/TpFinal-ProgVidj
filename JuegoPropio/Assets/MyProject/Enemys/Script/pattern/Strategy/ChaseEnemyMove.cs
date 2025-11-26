using UnityEngine;

public class ChaseEnemyMove : IMoveEnemyStrategy
{
    public void Move(Transform enemyTransform, Transform playerTransform, float speed)
    {
        if (playerTransform == null) return;

        Vector3 direction = playerTransform.position - enemyTransform.position;

        // movimiento solo en el plano XZ
        direction.y = 0;

        direction = direction.normalized;
        enemyTransform.position += direction * speed * Time.deltaTime;

        // Rotar hacia el jugador
        if (direction != Vector3.zero)
            enemyTransform.forward = direction;
    }
}
