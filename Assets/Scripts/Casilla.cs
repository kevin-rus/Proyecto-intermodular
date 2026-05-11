using PurrNet;
using System.Collections;
using UnityEngine;

// Objeto de las casilla del juego, guarda la ficha que tenga encima y gestiona los movimientos
public class Casilla : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    public BasePiece OccupiedPiece;
    private int posX, posY;

    // Elimina la ficha de la casilla previa y le asigna la nueva
    public void setPiece(BasePiece piece)
    {
        if (piece.OccupiedCasilla != null) piece.OccupiedCasilla.OccupiedPiece = null;
        piece.transform.position = transform.position;
        OccupiedPiece = piece;
        piece.OccupiedCasilla = this;
    }

    // Función puente que permite el correcto funcionamiento del Network
    public void inicioCambioColor(int x, int y)
    {
        Debug.Log("Init cambio color");
        posX = x;
        posY = y;

        StartCoroutine(nameof(AwaitCambio));
    }

    IEnumerator AwaitCambio()
    {
        Debug.Log("Init Corrutina");
        yield return new WaitUntil(() => observers.Count > 0);

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
        _renderer.color = esNegra ? Color.black : Color.white;
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
        else if ((GameManager.instance.state == GameState.BlackTurn) && !isServer)
        {
            moveBlack();
        }
    }

    // Mueve las piezas blancas
    [ObserversRpc]
    private void moveWhite()
    {
        // Comprueba que haya una pieza y sea blanaca
        if (OccupiedPiece != null && OccupiedPiece.player == Player.White)
        {
            Debug.Log("Seleccionando pieza blanca");
            PieceManager.instance.SetSelectedPiece((BaseWhite)OccupiedPiece);
        }
        else
        {
            // Se ejecuta una vez seleccionada la pieza
            if (PieceManager.instance.SelectedPiece != null)
            {
                // Establece la nueva casilla de la pieza

                // TODO: Aquí se puede hacer la comprobación del movimiento

                Debug.Log("Moviendo pieza blanca");
                setPiece(PieceManager.instance.SelectedPiece);
                PieceManager.instance.SetSelectedPiece(null);

                updateTurn();
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
            PieceManager.instance.SetSelectedPiece((BaseBlack)OccupiedPiece);
        }
        else
        {
            // Se ejecuta una vez seleccionada la pieza
            if (PieceManager.instance.SelectedPiece != null)
            {
                // Establece la nueva casilla de la pieza

                // TODO: Aquí se puede hacer la comprobación del movimiento

                Debug.Log("Moviendo pieza negra");
                setPiece(PieceManager.instance.SelectedPiece);
                PieceManager.instance.SetSelectedPiece(null);

                updateTurn();
            }
        }
    }

    // Actualiza el turno
    [ObserversRpc]
    private void updateTurn()
    {
        GameManager.instance.UpdateGameState(
            GameManager.instance.state == GameState.WhiteTurn ?
            GameState.BlackTurn : GameState.WhiteTurn);
    }
}