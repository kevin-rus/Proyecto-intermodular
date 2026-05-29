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
    public BasePiece WhitePawn, BlackPawn;
    public BasePiece WhiteRook, BlackRook;
    public BasePiece WhiteHorse, BlackHorse;
    public BasePiece WhiteBishop, BlackBishop;
    public BasePiece WhiteKing, BlackKing;
    public BasePiece WhiteQueen, BlackQueen;

    // Referencias a los reyes spawneados en partida
    private King _blackKing, _whiteKing;

    private void Awake()
    {
        instance = this;
    }

    // Carga las piezas blancas en el tablero
    public void SpawnWhites()
    {
        if (!isServer) return;

        var whiteCount = 8;

        // Genera los peones blancos
        for (int i = 0; i < whiteCount; i++)
        {
            var spawnedPeon = Instantiate(WhitePawn);
            var casilla = BoardManager.instance.GetTileFromPosition(new Vector2(i, 1));

            casilla.setPiece(spawnedPeon);
        }

        //Genera las torres blancas
        for (int i = 0; i < 2; i++)
        {
            var spawnedTorre = Instantiate(WhiteRook);
            var casilla = BoardManager.instance.GetTileFromPosition(new Vector2(i * 7, 0));

            casilla.setPiece(spawnedTorre);
        }
        
        // Genera los caballos blancos
        for (int i = 0; i < 2; i++)
        {
            var spawnedCaballo = Instantiate(WhiteHorse);
            var casilla = BoardManager.instance.GetTileFromPosition(new Vector2(1 + i * 5, 0));

            casilla.setPiece(spawnedCaballo);
        }

        // Genera los alfiles blancos
        for (int i = 0; i < 2; i++)
        {
            var spawnedAlfil = Instantiate(WhiteBishop);
            var casilla = BoardManager.instance.GetTileFromPosition(new Vector2(2 + i * 3, 0));

            casilla.setPiece(spawnedAlfil);
        }

        // Genera el rey blanco
        var spawnedRey = Instantiate(WhiteKing);
        var casillaRey = BoardManager.instance.GetTileFromPosition(new Vector2(4, 0));

        casillaRey.setPiece(spawnedRey);

        _whiteKing = (King)spawnedRey;

        // Genera la reina blanca
        var spawnedReina = Instantiate(WhiteQueen);
        var casillaReina = BoardManager.instance.GetTileFromPosition(new Vector2(3, 0));

        casillaReina.setPiece(spawnedReina);

        GameManager.instance.UpdateGameState(GameState.SpawnBlacks);
    }

    // Carga las piezas negras en el tablero
    public void SpawnBlacks()
    {
        if(!isServer) return;

        var blackCount = 8;

        // Genera los peones negros
        for (int i = 0; i < blackCount; i++)
        {
            var spawnedBlack = Instantiate(BlackPawn);
            var casilla = BoardManager.instance.GetTileFromPosition(new Vector2(i, 6));

            casilla.setPiece(spawnedBlack);
        }

        // Genera las torres negras
        for (int i = 0; i < 2; i++)
        {
            var spawnedTorre = Instantiate(BlackRook);
            var casilla = BoardManager.instance.GetTileFromPosition(new Vector2(i * 7, 7));

            casilla.setPiece(spawnedTorre);
        }

        // Genera los caballos negros
        for (int i = 0; i < 2; i++)
        {
            var spawnedCaballo = Instantiate(BlackHorse);
            var casilla = BoardManager.instance.GetTileFromPosition(new Vector2(1 + i * 5, 7));

            casilla.setPiece(spawnedCaballo);
        }

        // Genera los alfiles negros
        for (int i = 0; i < 2; i++)
        {
            var spawnedAlfil = Instantiate(BlackBishop);
            var casilla = BoardManager.instance.GetTileFromPosition(new Vector2(2 + i * 3, 7));

            casilla.setPiece(spawnedAlfil);
        }

        // Genera el rey negro
        var spawnedRey = Instantiate(BlackKing);
        var casillaRey = BoardManager.instance.GetTileFromPosition(new Vector2(4, 7));

        casillaRey.setPiece(spawnedRey);

        _blackKing = (King)spawnedRey;

        // Genera la reina negra
        var spawnedReina = Instantiate(BlackQueen);
        var casillaReina = BoardManager.instance.GetTileFromPosition(new Vector2(3, 7));

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
