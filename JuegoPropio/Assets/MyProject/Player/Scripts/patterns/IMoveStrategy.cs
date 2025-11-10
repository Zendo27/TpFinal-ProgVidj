using UnityEngine;

// Interfaz que define el contrato para las estrategias de movimiento
public interface IMoveStrategy
{
    void Move(Transform transform, float speed);
}
