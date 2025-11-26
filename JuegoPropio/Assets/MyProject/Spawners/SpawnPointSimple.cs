using UnityEngine;

public class SpawnPointSimple : MonoBehaviour
{
    [Header("Prefabs que puede spawnear este punto")]
    public GameObject[] enemyPrefabs;

    [Header("Configuración del spawn")]
    public float spawnInterval = 2f;
    public int maxEnemies = 5;

    private int spawnedCount = 0;
    private bool isSpawning = false;

    private void OnEnable()
    {
        // Cuando el spawner aparece en la escena, empieza a funcionar
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (!isSpawning)
            StartCoroutine(SpawnRoutine());
    }

    private System.Collections.IEnumerator SpawnRoutine()
    {
        isSpawning = true;

        while (spawnedCount < maxEnemies)
        {
            yield return new WaitForSeconds(spawnInterval);

            // elegir enemigo
            if (enemyPrefabs.Length == 0) continue;

            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // instanciar
            Instantiate(enemy, transform.position, Quaternion.identity);

            spawnedCount++;
        }

        isSpawning = false;
    }
}
