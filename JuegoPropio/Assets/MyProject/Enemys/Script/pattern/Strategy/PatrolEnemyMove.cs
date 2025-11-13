using UnityEngine;

public class PatrolEnemyMove : IMoveEnemyStrategy
{
    private Vector3 startPos;
    private float cronometro;
    private int rutina;
    private Quaternion angulo;
    private float grado;

    public PatrolEnemyMove(Vector3 origin)
    {
        startPos = origin;
    }

    public void Move(Transform enemyTransform, Transform playerTransform, float speed)
    {
        cronometro += Time.deltaTime;

        if (cronometro >= 4)
        {
            rutina = Random.Range(0, 2);
            cronometro = 0;
        }

        switch (rutina)
        {
            case 0:
                // Quieto
                break;

            case 1:
                // Decide nuevo ángulo aleatorio
                grado = Random.Range(0, 360);
                angulo = Quaternion.Euler(0, grado, 0);
                rutina++;
                break;

            case 2:
                // Gira y avanza
                enemyTransform.rotation = Quaternion.RotateTowards(enemyTransform.rotation, angulo, 0.5f);
                enemyTransform.Translate(Vector3.forward * speed * Time.deltaTime);
                break;
        }
    }

    // 🔁 Girar al colisionar
    public void OnCollisionWithWall(Transform enemyTransform)
    {
        // Gira 180 grados al chocar
        enemyTransform.Rotate(0, 180f, 0);
    }
}
