using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Bloquea el paso cuando la puerta está cerrada")]
    public Collider blocker;  // collider no-trigger

    [Header("Detecta al jugador para teletransportarlo")]
    public Collider trigger;  // collider trigger

    [Header("Punto donde aparece el player")]
    public Transform teleportTarget;

    private bool isOpen = false;

    private void Awake()
    {
        // Si no asignaste nada, busca automáticamente
        if (blocker == null)
            blocker = GetComponent<Collider>();

        if (trigger == null)
            trigger = GetComponentInChildren<Collider>();
    }

    public void Close()
    {
        isOpen = false;

        if (blocker != null) blocker.enabled = true;     // bloquea el paso
        if (trigger != null) trigger.enabled = false;    // evita teletransporte
    }

    public void Open()
    {
        isOpen = true;

        if (blocker != null) blocker.enabled = false;    // permite pasar
        if (trigger != null) trigger.enabled = true;     // habilita teletransporte
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpen) return; // 🚫 no teletransportar si está cerrada
        if (!other.CompareTag("Player")) return;
        if (teleportTarget == null) return;

        other.transform.position = teleportTarget.position;
        other.transform.rotation = teleportTarget.rotation;
    }
}
