using UnityEngine;

public class DiagonalBounceMove : IMoveEnemyStrategy
{
    private Vector3 direction;

    public DiagonalBounceMove()
    {
        // Dirección inicial diagonal
        direction = new Vector3(1f, 0f, 1f).normalized;
    }

    public void Move(Transform enemyTransform, Transform playerTransform, float speed)
    {
        enemyTransform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void OnCollisionWithWall(Transform enemyTransform, Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.transform.root.CompareTag("Ground"))
            return;

        Vector3 normal = collision.contacts[0].normal;

        normal.y = 0f;
        direction.y = 0f;

        direction = Vector3.Reflect(direction, normal).normalized;
    }

}
