using UnityEngine;

public class RoomSpawnerManager : MonoBehaviour
{
    [Header("Prefabs de Spawners")]
    public GameObject[] enemySpawnerPrefabs;

    [Header("Área donde pueden aparecer spawners")]
    public BoxCollider spawnArea;

    [Header("Cantidad aleatoria de spawners")]
    public int minSpawners = 3;
    public int maxSpawners = 4;

    private bool alreadySpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!alreadySpawned)
        {
            alreadySpawned = true;
            GenerateRandomSpawners();
        }
        Debug.Log("El jugador entró al Room. Generando spawners...");

    }

    void GenerateRandomSpawners()
    {
        if (spawnArea == null)
        {
            Debug.LogError("⚠ Asigná el BoxCollider 'spawnArea' en el inspector.");
            return;
        }

        int amount = Random.Range(minSpawners, maxSpawners + 1);

        for (int i = 0; i < amount; i++)
        {
            Vector3 randomPoint = GetRandomPointInArea();

            GameObject prefab = enemySpawnerPrefabs[Random.Range(0, enemySpawnerPrefabs.Length)];

            Instantiate(prefab, randomPoint, Quaternion.identity, transform);
        }
        Debug.Log("Generando " + amount + " spawners.");

    }

    Vector3 GetRandomPointInArea()
    {
        Vector3 center = spawnArea.bounds.center;
        Vector3 size = spawnArea.bounds.extents;

        float x = Random.Range(center.x - size.x, center.x + size.x);
        float z = Random.Range(center.z - size.z, center.z + size.z);
        float y = transform.position.y;

        return new Vector3(x, y, z);
    }
}
