using UnityEngine;

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
