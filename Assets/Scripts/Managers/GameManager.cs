using PurrNet;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Gestor central del juego, enfocado principalmente en controlar el sistema de turnos
public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    public Image whiteTurnIndicator, blackTurnIndicator;

    public Image checkIndicator;
    public TextMeshProUGUI checkIndicatorText;

    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        instance = this;
        whiteTurnIndicator.gameObject.SetActive(false);
        blackTurnIndicator.gameObject.SetActive(false);
        checkIndicator.gameObject.SetActive(false);
    }

    protected override void OnSpawned()
    {
        base.OnSpawned();
        UpdateGameState(GameState.GenerateTable);
    }

    public void UpdateGameState(GameState newState)
    {
        Debug.Log("Actualizando estado: " + newState);
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
                whiteTurnIndicator.gameObject.SetActive(true);
                blackTurnIndicator.gameObject.SetActive(false);
                lookForCheck();
                break;
            case GameState.BlackTurn:
                whiteTurnIndicator.gameObject.SetActive(false);
                blackTurnIndicator.gameObject.SetActive(true);
                lookForCheck();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
    }

    [ObserversRpc]
    public void lookForCheck()
    {
        if (PieceManager.instance.GetWhiteKing().inCheck)
        {
            checkIndicatorText.text = "Rey blanco en jaque!";
            checkIndicator.gameObject.SetActive(true);
        }
        else if (PieceManager.instance.GetBlackKing().inCheck)
        {
            checkIndicatorText.text = "Rey negro en jaque!";
            checkIndicator.gameObject.SetActive(true);
        }
        else
        {
            checkIndicator.gameObject.SetActive(false);
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
    BlackTurn,
}