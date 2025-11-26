using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public int CurrentEnemies = 0;

    public void RegisterEnemy()
    {
        CurrentEnemies++;
        Debug.Log("Registrado enemigo. Total ahora: " + CurrentEnemies);
    }

    public void EnemyDied()
    {
        CurrentEnemies--;
        Debug.Log("Enemigo murió. Total ahora: " + CurrentEnemies);
    }
}
