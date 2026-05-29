using PurrLobby;
using PurrNet;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Gestor central del juego, enfocado principalmente en controlar el sistema de turnos
public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    public Image whiteTurnIndicator, blackTurnIndicator;
    
    public Image checkMate;
    public TextMeshProUGUI winner;

    public Image checkIndicator;
    public TextMeshProUGUI checkIndicatorText;

    public Button returnToLobby;
    [PurrScene, SerializeField] private string nextScene;

    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        instance = this;

        whiteTurnIndicator.gameObject.SetActive(false);
        blackTurnIndicator.gameObject.SetActive(false);
        checkIndicator.gameObject.SetActive(false);
        checkMate.gameObject.SetActive(false);

        returnToLobby.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(nextScene);
        });
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
                BoardManager.instance.CalculateTileSize();
                BoardManager.instance.GenerateBoard();
                break;
            case GameState.ColorTable:
                BoardManager.instance.ChangeColor();
                break;
            case GameState.SpawnWhites:
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
            case GameState.CheckMate:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
    }

    [ObserversRpc]
    public void lookForCheck()
    {
        if(PieceManager.instance.GetWhiteKing().checkMate || PieceManager.instance.GetBlackKing().checkMate)
        {
            endGame();
        }
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

    [ServerRpc]
    public void endGame()
    {
        Debug.Log("Jaque Mate. Fin de la partida");
        UpdateGameState(GameState.CheckMate);

        if (!isServer) return;
        winner.text = PieceManager.instance.GetWhiteKing().checkMate ? "Negras ganan!" : "Blancas ganan!";
        
        displayEndGame();
    }

    [ObserversRpc]
    public void displayEndGame()
    {
        checkMate.gameObject.SetActive(true);
    }
}

// Los diferentes estados que puede tomar el juego
public enum GameState
{
    Start,
    GenerateTable,
    ColorTable,
    SpawnWhites,
    SpawnBlacks,
    WhiteTurn,
    BlackTurn,
    CheckMate,
}