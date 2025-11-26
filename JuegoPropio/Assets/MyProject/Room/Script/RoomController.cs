using UnityEngine;

public class RoomController : MonoBehaviour
{
    private bool firstEnter = false;
    private DoorController[] doors;
    private EnemyCounter enemyCounter;

    private void Awake()
    {
        // ⭐ Busca puertas SOLO dentro de esta habitación ⭐
        doors = GetComponentsInChildren<DoorController>();

        // ⭐ Busca el EnemyCounter solo dentro de la sala ⭐
        enemyCounter = GetComponentInChildren<EnemyCounter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!firstEnter && other.CompareTag("Player"))
        {
            firstEnter = true;
            CloseAllDoors();
        }
    }

    private void Update()
    {
        // ⭐ Si ya entró el jugador y NO quedan enemigos → abrir puertas ⭐
        if (firstEnter && enemyCounter != null && enemyCounter.CurrentEnemies <= 0)
        {
            OpenAllDoors();
        }
    }

    public void CloseAllDoors()
    {
        foreach (var d in doors)
            d?.CloseDoor();
    }

    public void OpenAllDoors()
    {
        foreach (var d in doors)
            d?.OpenDoor();
    }
}
