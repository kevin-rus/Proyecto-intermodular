using UnityEngine;

public class Casilla : MonoBehaviour
{
    public BasePiece OccupiedPiece;

    public void setPiece(BasePiece piece)
    {
        if(piece.OccupiedCasilla != null) piece.OccupiedCasilla.OccupiedPiece = null;
        piece.transform.position = transform.position;
        OccupiedPiece = piece;
        piece.OccupiedCasilla = this;
    }

    void OnMouseDown()
    {
        Debug.Log("Click + " + this.name);

        if (GameManager.instance.state == GameState.WhiteTurn)
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

        } else if (GameManager.instance.state == GameState.BlackTurn)
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
}
