using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Prefabs que puede spawnear este punto")]
    public List<GameObject> enemyPrefabs;

    [Header("Configuración del spawn")]
    public float spawnInterval = 2f;   // Tiempo entre spawns
    public int maxEnemies = 5;         // Cantidad máxima que puede generar
    private int spawnedCount = 0;

    public GameObject GetRandomEnemy()
    {
        if (enemyPrefabs.Count == 0) return null;
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
    }

    public bool CanSpawn() => spawnedCount < maxEnemies;

    public void IncrementCount() => spawnedCount++;
}
