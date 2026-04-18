using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.GenerateTable);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (state)
        {
            case GameState.GenerateTable:
                TableroManager.instance.CalcularTamañoCasilla();
                TableroManager.instance.GenerarTablero();
                break;
            case GameState.SpawnWthites:
                PieceManager.instance.SpawnWhites();
                break;
            case GameState.SpawnBlacks:
                PieceManager.instance.SpawnBlacks();
                break;
            case GameState.WhiteTurn:
                break;
            case GameState.BlackTurn:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}

public enum GameState
{
    GenerateTable,
    SpawnWthites,
    SpawnBlacks,
    WhiteTurn,
    BlackTurn
}