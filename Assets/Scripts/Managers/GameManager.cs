using PurrNet;
using System;
using UnityEngine;
using UnityEngine.UI;

//Gestor central del juego, enfocado principalmente en controlar el sistema de turnos
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

    // Cuando se presiona el botón, inicia la partida
    /*
     * Había un problema al cargar el tablero nada mas ejecutar el juego.
     * 
     * RESUMEN: El Network se ralla como intentes darle órdenes nada mas empezar.
     * El punto del botón es el de crear un delay que permita a la aplicación asentarse
     * y asegurar el correcto funcionamiento del sistema de red.
     */

    [ObserversRpc]
    public void StartGame()
    {
        startButton.gameObject.SetActive(false);
        UpdateGameState(GameState.GenerateTable);
    }
}

// Los diferentes estados que puede tomar el juego
public enum GameState
{
    Start,
    GenerateTable,
    SpawnWthites,
    SpawnBlacks,
    WhiteTurn,
    BlackTurn
}