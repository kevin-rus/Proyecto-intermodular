using PurrNet;
using System;
using UnityEngine;
using UnityEngine.UI;

//Gestor central del juego, enfocado principalmente en controlar el sistema de turnos
public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        instance = this;
    }

    protected override void OnSpawned()
    {
        base.OnSpawned();
        UpdateGameState(GameState.GenerateTable);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (state)
        {
            case GameState.Start:
                Debug.Log("Game Started");
                UpdateGameState(GameState.GenerateTable);
                break;
            case GameState.GenerateTable:
                TableroManager.instance.CalcularTamañoCasilla();
                TableroManager.instance.GenerarTablero();
                break;
            case GameState.ColorTable:
                TableroManager.instance.cambiarColor();
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

// Los diferentes estados que puede tomar el juego
public enum GameState
{
    Start,
    GenerateTable,
    ColorTable,
    SpawnWthites,
    SpawnBlacks,
    WhiteTurn,
    BlackTurn
}