using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Collider col;
    private Renderer rend;

    private void Awake()
    {
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
    }

    public void CloseDoor()
    {
        if (col != null) col.enabled = true;
        if (rend != null) rend.enabled = true;

        // Aquí podés meter animación si querés
    }

    public void OpenDoor()
    {
        if (col != null) col.enabled = false;
        if (rend != null) rend.enabled = false;

        // Animaciones opcionales
    }
}
