using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform teleportTarget;   // Donde aparece el player
    public Transform cameraPoint;      // Dónde debe colocarse la cámara en la nueva habitación

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && teleportTarget != null)
        {
            // Teletransportar al jugador
            other.transform.position = teleportTarget.position;

            // Mover la cámara
            MoveCamera();
        }
    }

    private void MoveCamera()
    {
        if (cameraPoint == null) return;

        Camera.main.transform.position = cameraPoint.position;
        Camera.main.transform.rotation = cameraPoint.rotation;
    }
}
