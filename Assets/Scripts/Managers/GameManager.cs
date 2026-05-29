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

    public TextMeshProUGUI thisPlayerText;

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

    [ObserversRpc]
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
                if (isServer)
                {
                    PieceManager.instance.SpawnWhites();
                }
                break;
            case GameState.SpawnBlacks:
                if (isServer)
                {
                    PieceManager.instance.SpawnBlacks();
                }
                break;
            case GameState.SetPlayer:
                thisPlayerText.text = "Juegas como: " + (isServer ? "Blancas" : "Negras");
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

    [ServerRpc]
    public void lookForCheck()
    {
        if (PieceManager.instance.GetWhiteKing().checkMate || PieceManager.instance.GetBlackKing().checkMate)
        {
            endGame();
        }
        if (PieceManager.instance.GetWhiteKing().inCheck)
        {
            displayCheck(true, Player.White);
        }
        else if (PieceManager.instance.GetBlackKing().inCheck)
        {
            displayCheck(true, Player.Black);
        }
        else
        {
            displayCheck(false, Player.White);  // Recibe un Player.White porque necesita de un placeholder, pero realmente no se va a usar
        }
    }

    [ServerRpc]
    public void endGame()
    {
        Debug.Log("Jaque Mate. Fin de la partida");
        UpdateGameState(GameState.CheckMate);

        if (!isServer) return;
        Player playerInCheck = PieceManager.instance.GetWhiteKing().checkMate ? Player.White : Player.Black;
        
        displayEndGame(playerInCheck);
    }

    [ObserversRpc]
    public void displayEndGame(Player player)
    {
        winner.text = player == Player.White ? "Negras ganan!" : "Blancas ganan!";
        checkMate.gameObject.SetActive(true);
    }

    [ObserversRpc]
    public void displayCheck(bool inCheck, Player player)
    {
        if(!inCheck)
        {
            checkIndicator.gameObject.SetActive(false);
        }
        else
        {
            checkIndicatorText.text = player == Player.White ? "Rey blanco en jaque!" : "Rey negro en jaque!";
            checkIndicator.gameObject.SetActive(true);
        }
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
    SetPlayer,
    WhiteTurn,
    BlackTurn,
    CheckMate,
}