using PurrNet;
using UnityEngine;

public class Casilla : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    public BasePiece OccupiedPiece;

    public void setPiece(BasePiece piece)
    {
        if (piece.OccupiedCasilla != null) piece.OccupiedCasilla.OccupiedPiece = null;
        piece.transform.position = transform.position;
        OccupiedPiece = piece;
        piece.OccupiedCasilla = this;
    }

    public void cambiarColor(int x, int y)
    {
        this.name = $"Casilla {x} {y}";

        _renderer = GetComponent<SpriteRenderer>();

        bool esNegra = (x + y) % 2 == 1;
        _renderer.color = esNegra ? Color.black : Color.white;
        Debug.Log("Se han cambiado los colores");
    }

    void OnMouseDown()
    {
        Debug.Log("Click + " + this.name);

        if ((GameManager.instance.state == GameState.WhiteTurn))
        {
            moveWhite();

        }
        else if ((GameManager.instance.state == GameState.BlackTurn))
        {
            moveBlack();
        }
    }

    private void moveWhite()
    {
        if (OccupiedPiece != null && OccupiedPiece.player == Player.White)
        {
            PieceManager.instance.SetSelectedPiece((BaseWhite)OccupiedPiece);
        }
        else
        {
            if (PieceManager.instance.SelectedPiece != null)
            {
                setPiece(PieceManager.instance.SelectedPiece);
                PieceManager.instance.SetSelectedPiece(null);

                GameManager.instance.UpdateGameState(GameState.BlackTurn);
            }
        }
    }

    private void moveBlack()
    {
        if (OccupiedPiece != null && OccupiedPiece.player == Player.Black)
        {
            PieceManager.instance.SetSelectedPiece((BaseBlack)OccupiedPiece);
        }
        else
        {
            if (PieceManager.instance.SelectedPiece != null)
            {
                setPiece(PieceManager.instance.SelectedPiece);
                PieceManager.instance.SetSelectedPiece(null);

                GameManager.instance.UpdateGameState(GameState.WhiteTurn);
            }
        }
    }
}