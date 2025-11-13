using UnityEngine;

public class RangedAttack : IAttackStrategy
{
    private GameObject bulletPrefab;
    private Transform firePoint;
    private float shootCooldown = 0.5f;
    private float lastShootTime = 0f;
    private float attackDistance = 10f;

    public RangedAttack(GameObject EnemyBullet, Transform enemyTransform, float distance = 10f)
    {
        bulletPrefab = EnemyBullet;
        attackDistance = distance;

        firePoint = enemyTransform.Find("EnemyFirePoint");

        if (firePoint == null)
        {
            Debug.LogWarning($"No se encontró 'EnemyFirePoint' como hijo de {enemyTransform.name}");
        }
    }


    public void Attack(Transform enemyTransform, Transform playerTransform)
    {
        float distance = Vector3.Distance(enemyTransform.position, playerTransform.position);
        if (distance > attackDistance) return; // No dispara si está lejos

        if (Time.time - lastShootTime < shootCooldown)
            return;

        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning($"Falta asignar bulletPrefab o firePoint en el enemigo {enemyTransform.name}.");
            return;
        }

        // Calcula la dirección hacia el jugador
        Vector3 direction = (playerTransform.position - firePoint.position).normalized;

        // Instancia la bala en el punto de disparo
        GameObject bullet = GameObject.Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Asigna la dirección a la bala
        var enemyBullet = bullet.GetComponent<EnemyBullet>();
        if (enemyBullet != null)
        {
            enemyBullet.SetDirection(direction);
            enemyBullet.SetTarget(playerTransform);
        }

        lastShootTime = Time.time;
    }
}
