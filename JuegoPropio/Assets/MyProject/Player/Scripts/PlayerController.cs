using JetBrains.Annotations;
using UnityEngine;

// Controlador principal del jugador
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // velocidad editable en Inspector
    [SerializeField] private float jumpForce = 5f; // Fuerza de salto
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private GameObject bulletPrefab; // Prefab de la bala
    [SerializeField] private Transform firePoint;     // Punto de salida de la bala

    private IMoveStrategy moveStrategy;
    private Rigidbody rb;
    private bool isSprinting = false;


    private void Start()
    {
        //Inicializa componentes
        rb = GetComponent<Rigidbody>();
        // Por defecto: movimiento suave en global
        moveStrategy = new SmoothGlobalMove();
        
    }

    private void Update()
    {
        // Movimiento básico usando la estrategia actual
        moveStrategy.Move(transform, GetCurrentSpeed());
        // Detectar input WASD
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Si hay input, el player se está moviendo
        bool isMoving = (horizontal != 0 || vertical != 0);

        // Ejecutar la estrategia
        moveStrategy.Move(transform, speed);

        // Cambiar entre modos con teclas (para pruebas)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            moveStrategy = new SmoothGlobalMove();
            Debug.Log("Modo: Movimiento suave global");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            moveStrategy = new FastGlobalMove();
            Debug.Log("Modo: Movimiento brusco global");
        }
    }

    // Devuelve la velocidad actual
    private float GetCurrentSpeed()
    {
        return isSprinting ? speed * sprintMultiplier : speed;
    }

    // Activar/desactivar sprint
    public void StartSprint()
    {
        isSprinting = true;
        Debug.Log("Sprint activado");
    }

    public void StopSprint()
    {
        isSprinting = false;
        Debug.Log("Sprint desactivado");
    }
    public void Jump()
    {
        // Solo saltar si está tocando el suelo
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    // Método auxiliar para saber si está tocando el suelo
    private bool IsGrounded()
    {
        // Raycast hacia abajo
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    public void Shoot(Vector3 direction)
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Falta asignar el bulletPrefab o el firePoint en el inspector.");
            return;
        }

        // Instancia la bala y establece su dirección
        GameObject bullet = GameObject.Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }
}
