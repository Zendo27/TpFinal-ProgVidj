using UnityEngine;

// Comando concreto: hace que el jugador salte
public class JumpCommand : ICommand
{
    private PlayerController player; // receptor del comando

    public JumpCommand(PlayerController player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.Jump(); // delega la acción real al PlayerController
    }
}
