using UnityEngine;

public class ChaseEnemyMove : IMoveEnemyStrategy
{
    public void Move(Transform enemyTransform, Transform playerTransform, float speed)
    {
        if (playerTransform == null) return;

        Vector3 direction = (playerTransform.position - enemyTransform.position).normalized;
        enemyTransform.position += direction * speed * Time.deltaTime;

        // Opcional: rotar hacia el jugador
        if (direction != Vector3.zero)
        {
            enemyTransform.forward = direction;
        }
    }
}
