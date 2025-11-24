using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private SpawnPoint[] spawnPoints;

    void Start()
    {
        // Busca automáticamente todos los objetos con el tag "Spawner"
        GameObject[] spawnerObjects = GameObject.FindGameObjectsWithTag("Spawner");

        // Convierte esos objetos a una lista de SpawnPoint
        spawnPoints = new SpawnPoint[spawnerObjects.Length];
        for (int i = 0; i < spawnerObjects.Length; i++)
        {
            spawnPoints[i] = spawnerObjects[i].GetComponent<SpawnPoint>();
        }

        // Inicia las coroutines
        foreach (var sp in spawnPoints)
        {
            if (sp != null)
                StartCoroutine(SpawnRoutine(sp));
        }
    }

    private System.Collections.IEnumerator SpawnRoutine(SpawnPoint sp)
    {
        while (true)
        {
            yield return new WaitForSeconds(sp.spawnInterval);

            if (sp.CanSpawn())
            {
                GameObject enemy = sp.GetRandomEnemy();
                if (enemy != null)
                {
                    Instantiate(enemy, sp.transform.position, Quaternion.identity);
                    sp.IncrementCount();
                }
            }
        }
    }
}
