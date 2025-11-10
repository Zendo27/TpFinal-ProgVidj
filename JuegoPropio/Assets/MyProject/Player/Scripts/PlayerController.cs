using UnityEngine;

// Controlador principal del jugador
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // velocidad editable en Inspector

    private IMoveStrategy moveStrategy;

    private void Start()
    {
        // Por defecto: movimiento suave en global
        moveStrategy = new SmoothGlobalMove();
    }

    private void Update()
    {
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
}
