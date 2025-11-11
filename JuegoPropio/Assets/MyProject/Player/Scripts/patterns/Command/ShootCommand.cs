using UnityEngine;

// Comando que dispara una bala en una dirección específica
public class ShootCommand : ICommand
{
    private PlayerController player;
    private Vector3 shootDirection;

    public ShootCommand(PlayerController player, Vector3 direction)
    {
        this.player = player;
        this.shootDirection = direction;
    }

    public void Execute()
    {
        player.Shoot(shootDirection);
    }
}
