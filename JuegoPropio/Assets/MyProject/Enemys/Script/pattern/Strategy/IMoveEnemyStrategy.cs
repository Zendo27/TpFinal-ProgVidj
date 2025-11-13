using UnityEngine;

// Interfaz para estrategias de movimiento del enemigo
public interface IMoveEnemyStrategy
{
    void Move(Transform enemyTransform, Transform playerTransform, float speed);
}
