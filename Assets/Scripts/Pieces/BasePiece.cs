using UnityEngine;

// Clase base para las piezas
public class BasePiece : MonoBehaviour
{
    public Casilla OccupiedCasilla;     // Casilla que ocupa
    public Player player;               // Jugador al que pertenece
}

// BaseBlack y BaseWhite heredan esta clase