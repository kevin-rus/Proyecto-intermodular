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
    public BasePiece PeonBlanco, PeonNegro;
    public BasePiece TorreBlanca, TorreNegra;
    public BasePiece CaballoBlanco, CaballoNegro;
    public BasePiece AlfilBlanco, AlfilNegro;
    public BasePiece ReyBlanco, ReyNegro;
    public BasePiece ReinaBlanca, ReinaNegra;

    // Referencias a los reyes spawneados en partida
    private King _blackKing, _whiteKing;

    private void Awake()
    {
        instance = this;
    }

    // Carga las piezas blancas en el tablero
    public void SpawnWhites()
    {
        var whiteCount = 8;

        // Genera los peones blancos
        for (int i = 0; i < whiteCount; i++)
        {
            var spawnedPeon = Instantiate(PeonBlanco);
            var casilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(i, 1));

            casilla.setPiece(spawnedPeon);
        }

        //Genera las torres blancas
        for (int i = 0; i < 2; i++)
        {
            var spawnedTorre = Instantiate(TorreBlanca);
            var casilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(i * 7, 0));

            casilla.setPiece(spawnedTorre);
        }
        
        // Genera los caballos blancos
        for (int i = 0; i < 2; i++)
        {
            var spawnedCaballo = Instantiate(CaballoBlanco);
            var casilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(1 + i * 5, 0));

            casilla.setPiece(spawnedCaballo);
        }

        // Genera los alfiles blancos
        for (int i = 0; i < 2; i++)
        {
            var spawnedAlfil = Instantiate(AlfilBlanco);
            var casilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(2 + i * 3, 0));

            casilla.setPiece(spawnedAlfil);
        }

        // Genera el rey blanco
        var spawnedRey = Instantiate(ReyBlanco);
        var casillaRey = TableroManager.instance.GetCaillaFromPosition(new Vector2(4, 0));

        casillaRey.setPiece(spawnedRey);

        _whiteKing = (King)spawnedRey;

        // Genera la reina blanca
        var spawnedReina = Instantiate(ReinaBlanca);
        var casillaReina = TableroManager.instance.GetCaillaFromPosition(new Vector2(3, 0));

        casillaReina.setPiece(spawnedReina);

        GameManager.instance.UpdateGameState(GameState.SpawnBlacks);
    }

    // Carga las piezas negras en el tablero
    public void SpawnBlacks()
    {
        var blackCount = 8;

        // Genera los peones negros
        for (int i = 0; i < blackCount; i++)
        {
            var spawnedBlack = Instantiate(PeonNegro);
            var casilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(i, 6));

            casilla.setPiece(spawnedBlack);
        }

        // Genera las torres negras
        for (int i = 0; i < 2; i++)
        {
            var spawnedTorre = Instantiate(TorreNegra);
            var casilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(i * 7, 7));

            casilla.setPiece(spawnedTorre);
        }

        // Genera los caballos negros
        for (int i = 0; i < 2; i++)
        {
            var spawnedCaballo = Instantiate(CaballoNegro);
            var casilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(1 + i * 5, 7));

            casilla.setPiece(spawnedCaballo);
        }

        // Genera los alfiles negros
        for (int i = 0; i < 2; i++)
        {
            var spawnedAlfil = Instantiate(AlfilNegro);
            var casilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(2 + i * 3, 7));

            casilla.setPiece(spawnedAlfil);
        }

        // Genera el rey negro
        var spawnedRey = Instantiate(ReyNegro);
        var casillaRey = TableroManager.instance.GetCaillaFromPosition(new Vector2(4, 7));

        casillaRey.setPiece(spawnedRey);

        _blackKing = (King)spawnedRey;

        // Genera la reina negra
        var spawnedReina = Instantiate(ReinaNegra);
        var casillaReina = TableroManager.instance.GetCaillaFromPosition(new Vector2(3, 7));

        casillaReina.setPiece(spawnedReina);

        GameManager.instance.UpdateGameState(GameState.WhiteTurn);
    }

    // Establece la pieza seleccionada
    public void SetSelectedPiece(BasePiece piece)
    {
        SelectedPiece = piece;
    }

    public King GetWhiteKing()
    {
        return _whiteKing;
    }

    public King GetBlackKing()
    {
        return _blackKing;
    }
}
