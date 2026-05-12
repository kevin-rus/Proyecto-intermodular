using UnityEngine;

// Clase base para las piezas
public abstract class BasePiece : MonoBehaviour
{
    public Casilla OccupiedCasilla;             // Casilla que ocupa
    public Player player;                       // Jugador al que pertenece
    public Vector2[][] movimientosPosibles;     // Array en el que se registran los posibles movimientos de la ficha en un turno

    // Calcula los posibles movimientos de la ficha, dependiendo de su tipo y posición actual
    // El métedo está sujeto a cambios, según que clase de ficha se trate
    public abstract bool calcularMovimientos(Casilla casillaInic, Casilla casillaDese);
}

// BaseBlack y BaseWhite heredan esta clase