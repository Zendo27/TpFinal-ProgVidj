using UnityEngine;

public class IdleEnemyMove : IMoveEnemyStrategy
{
    public void Move(Transform enemyTransform, Transform playerTransform, float speed)
    {
        // No hace nada, el enemigo se queda quieto
    }
}
