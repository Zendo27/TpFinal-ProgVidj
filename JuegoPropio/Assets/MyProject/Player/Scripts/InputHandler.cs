using UnityEngine;

// Clase que maneja la entrada del jugador y ejecuta comandos
public class InputHandler : MonoBehaviour
{
    private ICommand jumpCommand;

    private PlayerController player;

    private void Start()
    {
        // Encontramos al jugador en la escena
        player = FindFirstObjectByType<PlayerController>();

        // Creamos el comando de salto
        jumpCommand = new JumpCommand(player);
    }

    private void Update()
    {
        // Si presiona Espacio, ejecutar el salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpCommand.Execute();
        }

        // Si presiona Shift, activar sprint
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ICommand sprintCommand = new SprintCommand(player, true);
            sprintCommand.Execute();
        }

        // Si suelta Shift, desactivar sprint
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ICommand stopSprintCommand = new SprintCommand(player, false);
            stopSprintCommand.Execute();
        }
        // Disparos con flechas
        if (Input.GetKeyDown(KeyCode.UpArrow))
            new ShootCommand(player, Vector3.forward).Execute();

        if (Input.GetKeyDown(KeyCode.DownArrow))
            new ShootCommand(player, Vector3.back).Execute();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            new ShootCommand(player, Vector3.left).Execute();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            new ShootCommand(player, Vector3.right).Execute();
    

    }
}
