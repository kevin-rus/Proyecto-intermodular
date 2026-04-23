using PurrNet;
using System.Collections;
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

    public void inicioCambioColor(int x, int y)
    {
        Debug.Log("inicio cambio de color");
        
        cambiarColor(x, y);
    }

    [ObserversRpc]
    public void cambiarColor(int x, int y)
    {
        Debug.Log($"Ejecutando cambio de color en casilla: {x} {y}");

        this.name = $"Casilla {x} {y}";

        _renderer = GetComponent<SpriteRenderer>();

        bool esNegra = (x + y) % 2 == 1;
        _renderer.color = esNegra ? Color.black : Color.white;
        Debug.Log("Se han cambiado los colores");
    }


    void OnMouseDown()
    {
        Debug.Log("Click + " + this.name);
        Debug.Log("Turno: " + GameManager.instance.state);

        if ((GameManager.instance.state == GameState.WhiteTurn) && isServer)
        {
            moveWhite();

        }
        else if ((GameManager.instance.state == GameState.BlackTurn) && !isServer)
        {
            moveBlack();
        }
    }

    [ObserversRpc]
    private void moveWhite()
    {
        if (OccupiedPiece != null && OccupiedPiece.player == Player.White)
        {
            Debug.Log("Seleccionando pieza blanca");
            PieceManager.instance.SetSelectedPiece((BaseWhite)OccupiedPiece);
        }
        else
        {
            if (PieceManager.instance.SelectedPiece != null)
            {
                Debug.Log("Moviendo pieza blanca");
                setPiece(PieceManager.instance.SelectedPiece);
                PieceManager.instance.SetSelectedPiece(null);

                updateTurn();
            }
        }
    }

    [ObserversRpc]
    private void moveBlack()
    {
        if (OccupiedPiece != null && OccupiedPiece.player == Player.Black)
        {
            Debug.Log("Seleccionando pieza negra");
            PieceManager.instance.SetSelectedPiece((BaseBlack)OccupiedPiece);
        }
        else
        {
            if (PieceManager.instance.SelectedPiece != null)
            {
                Debug.Log("Moviendo pieza negra");
                setPiece(PieceManager.instance.SelectedPiece);
                PieceManager.instance.SetSelectedPiece(null);

                updateTurn();
            }
        }
    }

    [ObserversRpc]
    private void updateTurn()
    {
        GameManager.instance.UpdateGameState(
            GameManager.instance.state == GameState.WhiteTurn ?
            GameState.BlackTurn : GameState.WhiteTurn);
    }
}