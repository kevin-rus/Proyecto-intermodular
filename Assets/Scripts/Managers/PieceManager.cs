using NUnit.Framework;
using PurrNet;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Gestor central de las piezas
public class PieceManager : NetworkBehaviour
{
    public static PieceManager instance;

    private List<ScriptablePieces> _pieces;

    public BasePiece SelectedPiece;
    public BasePiece PeonBlanco;
    public BasePiece PeonNegro;

    private void Awake()
    {
        instance = this;

        // Carga todas las piezas de la carpeta Assets/Resources
        /*
         * La carpeta de /Resources tenía una forma única de cargar prefabas
         * pero no me acuerdo muy bien de como funcionaba, el vídeo de Grid-vid-p2
         * en el doc de las tareas explicaba su funcionamiento
        */
        _pieces = Resources.LoadAll<ScriptablePieces>("Pieces").ToList();
    }

    // Carga las piezas blancas en el tablero
    public void SpawnWhites()
    {
        var whiteCount = 8;

        for (int i = 0; i < whiteCount; i++)
        {
            var spawnedWhite = Instantiate(PeonBlanco);
            var casilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(i, 1));

            Debug.Log("Estableciendo pieza en la casilla: " + i + " - " + 1);
            casilla.setPiece(spawnedWhite);
        }

        GameManager.instance.UpdateGameState(GameState.SpawnBlacks);
    }

    // Carga las piezas negras en el tablero
    public void SpawnBlacks()
    {
        var blackCount = 8;

        for (int i = 0; i < blackCount; i++)
        {
            var spawnedBlack = Instantiate(PeonNegro);
            var casilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(i, 6));

            Debug.Log("Estableciendo pieza en la casilla: " + i + " - " + 1);
            casilla.setPiece(spawnedBlack);
        }

        GameManager.instance.UpdateGameState(GameState.WhiteTurn);
    }

    // Establece la pieza seleccionada
    public void SetSelectedPiece(BasePiece piece)
    {
        SelectedPiece = piece;
    }
}
