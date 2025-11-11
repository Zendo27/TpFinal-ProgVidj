using UnityEngine;

// Comando para controlar el sprint del jugador
public class SprintCommand : ICommand
{
    private PlayerController player;
    private bool activateSprint;

    public SprintCommand(PlayerController player, bool activateSprint)
    {
        this.player = player;
        this.activateSprint = activateSprint;
    }

    public void Execute()
    {
        if (activateSprint)
            player.StartSprint(); // Activa el sprint
        else
            player.StopSprint();  // Desactiva el sprint
    }
}
