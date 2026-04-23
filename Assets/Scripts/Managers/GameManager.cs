using PurrNet;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    public GameState state;
    public Button startButton;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.Start);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (state)
        {
            case GameState.Start:
                break;
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

    [ObserversRpc]
    public void StartGame()
    {
        startButton.gameObject.SetActive(false);
        UpdateGameState(GameState.GenerateTable);
    }
}

public enum GameState
{
    Start,
    GenerateTable,
    SpawnWthites,
    SpawnBlacks,
    WhiteTurn,
    BlackTurn
}