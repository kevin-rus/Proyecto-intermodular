using UnityEngine;

// Voy a ser honesto, no se para que sirve esta clase
// Creo que tenía algo que ver con la carpeta /Resources
[CreateAssetMenu(fileName = "New Piece", menuName = "Scriptable Piece")]
public class ScriptablePieces : ScriptableObject
{
    public Player player;
    public BasePiece piece;
}

public enum Player
{
    White = 0,
    Black = 1,
}
