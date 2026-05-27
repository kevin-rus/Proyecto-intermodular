using PurrNet;
using System.Collections;
using UnityEngine;

// Objeto de las casilla del juego, guarda la ficha que tenga encima y gestiona los movimientos
public class Tile : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    public BasePiece OccupiedPiece;
    private int posX, posY;

    // Elimina la ficha de la casilla previa y le asigna la nueva
    public void setPiece(BasePiece piece)
    {
        if (piece.OccupiedTile != null) piece.OccupiedTile.OccupiedPiece = null;
        piece.transform.position = transform.position;
        OccupiedPiece = piece;
        piece.OccupiedTile = this;
    }

    // Función puente que permite el correcto funcionamiento del Network
    public void inicioCambioColor(int x, int y)
    {
        Debug.Log("Init cambio color");
        posX = x;
        posY = y;

        cambiarColor(posX, posY);
    }

    // Cambia el color de la casilla
    [ObserversRpc]
    public void cambiarColor(int x, int y)
    {
        Debug.Log($"Ejecutando cambio de color en casilla: {x} {y}");

        this.name = $"Casilla {x} {y}";

        _renderer = GetComponent<SpriteRenderer>();

        bool esNegra = (x + y) % 2 == 1;
        _renderer.color = esNegra ? new Color32(0x4F, 0x30, 0x16, 0xFF) : new Color32(0xFD, 0xE7, 0xD5, 0xFF);
        Debug.Log("Se han cambiado los colores");
    }

    // Detecta los clicks y comprueba el turno y el jugador
    void OnMouseDown()
    {
        Debug.Log("Click + " + this.name);
        Debug.Log("Turno: " + GameManager.instance.state);

        // Si es turno de blancas, actua el Servidor
        if ((GameManager.instance.state == GameState.WhiteTurn) && isServer)
        {
            moveWhite();
        }
        // Si es turno de negras, actua el Cliente
        else if ((GameManager.instance.state == GameState.BlackTurn) && isServer)
        {
            moveBlack();
        }
    }

    // Mueve las piezas blancas
    [ObserversRpc]
    private void moveWhite()
    {
        // Comprueba que haya una pieza y sea blanaca
        if (OccupiedPiece != null && OccupiedPiece.player == Player.White &&
        !(PieceManager.instance.SelectedPiece is King && OccupiedPiece is Rook))
        {
            Debug.Log("Seleccionando pieza blanca");
            PieceManager.instance.SetSelectedPiece((BasePiece)OccupiedPiece);
        }
        else
        {
            // Se ejecuta una vez seleccionada la pieza
            if (PieceManager.instance.SelectedPiece != null)
            {
                // Se calculan los movimientos posibles de la pieza seleccionada
                BasePiece pieza = PieceManager.instance.SelectedPiece;
                bool sePuedeMover = pieza.calculateMovements(pieza.OccupiedTile, this);

                // Si el movimiento es válido, se mueve la pieza a la nueva casilla
                if (sePuedeMover)
                {
                    Debug.Log("Moviendo pieza blanca");
                    setPiece(PieceManager.instance.SelectedPiece);

                    if(pieza is Pawn)
                    {
                        Pawn pawn = (Pawn)pieza;
                        pawn.promote();
                    }
                    
                    PieceManager.instance.SetSelectedPiece(null);

                    PieceManager.instance.GetBlackKing().isInCheck();
                    PieceManager.instance.GetWhiteKing().isInCheck();

                    Debug.Log("Fin turno blanco");
                    updateTurn();
                }
            }
        }
    }

    // Mueve las piezas negras
    [ObserversRpc]
    private void moveBlack()
    {
        // Comprueba que haya una pieza y sea negra
        if (OccupiedPiece != null && OccupiedPiece.player == Player.Black)
        {
            Debug.Log("Seleccionando pieza negra");
            PieceManager.instance.SetSelectedPiece((BasePiece)OccupiedPiece);
        }
        else
        {
            // Se ejecuta una vez seleccionada la pieza
            if (PieceManager.instance.SelectedPiece != null)
            {
                // Establece la nueva casilla de la pieza

                // Se calculan los movimientos posibles de la pieza seleccionada
                BasePiece pieza = PieceManager.instance.SelectedPiece;
                bool sePuedeMover = pieza.calculateMovements(pieza.OccupiedTile, this);

                // Si el movimiento es válido, se mueve la pieza a la nueva casilla
                if (sePuedeMover)
                {
                    Debug.Log("Moviendo pieza negra");
                    setPiece(pieza);

                    if(pieza is Pawn)
                    {
                        Pawn pawn = (Pawn)pieza;
                        pawn.promote();
                    }
                    PieceManager.instance.SetSelectedPiece(null);

                    PieceManager.instance.GetBlackKing().isInCheck();
                    PieceManager.instance.GetWhiteKing().isInCheck();

                    Debug.Log("Fin turno negro");
                    updateTurn();
                }
            }
        }
    }

    // Actualiza el turno
    [ObserversRpc]
    private void updateTurn()
    {
        Debug.Log("Actualizando turno");
        Debug.Log(GameManager.instance.state);
        GameManager.instance.UpdateGameState(
            GameManager.instance.state == GameState.WhiteTurn ?
            GameState.BlackTurn : GameState.WhiteTurn
        );
    }

    public int getPosX()
    {
        return posX;
    }

    public int getPosY()
    {
        return posY;
    }
}