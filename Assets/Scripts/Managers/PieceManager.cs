using NUnit.Framework;
using PurrNet;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceManager : NetworkBehaviour
{
    public static PieceManager instance;

    private List<ScriptablePieces> _pieces;

    public BasePiece SelectedPiece;

    private void Awake()
    {
        instance = this;

        _pieces = Resources.LoadAll<ScriptablePieces>("Pieces").ToList();
    }

    public void SpawnWhites()
    {
        var whiteCount = 1;

        for(int i = 0; i < whiteCount; i++)
        {
            var testWhite = _pieces.Where(u => u.player == Player.White).First().piece;
            var spawnedWhite = Instantiate(testWhite);
            var casilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(3, 0));

            casilla.setPiece(spawnedWhite);
        }

        GameManager.instance.UpdateGameState(GameState.SpawnBlacks);
    }

    public void SpawnBlacks()
    {
        var blackCount = 1;

        for (int i = 0; i < blackCount; i++)
        {
            var testBlack = _pieces.Where(u => u.player == Player.Black).First().piece;
            var spawnedBlack = Instantiate(testBlack);
            var casilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(3, 7));

            casilla.setPiece(spawnedBlack);
        }

        GameManager.instance.UpdateGameState(GameState.WhiteTurn);
    }

    public void SetSelectedPiece(BasePiece piece)
    {
        SelectedPiece = piece;
    }
}
