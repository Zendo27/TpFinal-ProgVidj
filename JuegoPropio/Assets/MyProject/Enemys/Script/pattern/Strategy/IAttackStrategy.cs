using UnityEngine;

public interface IAttackStrategy
{
    void Attack(Transform enemyTransform, Transform playerTransform);
}
