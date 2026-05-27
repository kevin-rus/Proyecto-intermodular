using PurrNet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableroManager : NetworkBehaviour
{
    public static TableroManager instance;
    // Prefab de una casilla (un sprite cuadrado con SpriteRenderer)
    [SerializeField] private Tile _casillaPrefab;

    // Guarda los datos de cada casilla para que sean accesibles en cualquier momento
    private Dictionary<Vector2, Tile> _casillas; 

    // Porcentaje de la altura de la pantalla que ocupará el tablero (0.1 = 10%, 1 = 100%)
    [Range(0.1f, 1f)]
    public float porcentajeAlturaPantalla = 0.8f; // 80% por defecto

    // Tamaño de cada casilla en unidades 
    float tamañoCasilla;

    private void Awake()
    {
        instance = this;
    }

    // Calcula el tamaño de cada casilla en función de la cámara y del porcentaje de pantalla
    public void CalcularTamañoCasilla()
    {
        Camera cam = Camera.main;

        // Altura del mundo visible por la cámara ortográfica
        float alturaMundo = 2f * cam.orthographicSize;

        // El tablero ocupa un porcentaje de esa altura
        float alturaTablero = alturaMundo * porcentajeAlturaPantalla;

        // Como el tablero es de 8x8, dividimos esa altura entre 8 para obtener el tamaño de una casilla
        tamañoCasilla = alturaTablero / 8f;
    }

    // Genera las 64 casillas del tablero
    public void GenerarTablero()
    {
        Debug.Log("Init generar tablero");
        // Si la instancia es un cliente, salta al estado de turno de blancas y espera al server
        if(!isServer)
        {
            Debug.Log("Is not server");
            await();
            return;
        }

        _casillas = new Dictionary<Vector2, Tile>();

        // Ancho y alto del tablero en unidades del mundo
        float anchoTablero = 8f * tamañoCasilla;
        float altoTablero = 8f * tamañoCasilla;

        // Punto de inicio (esquina inferior izquierda) para centrar el tablero en el origen (0,0)
        float xInicio = -anchoTablero / 2f;
        float yInicio = -altoTablero / 2f;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                // Calculamos la posición de cada casilla, sumando medio tamaño para centrarla en su "celda"
                Vector3 posicion = new Vector3(
                    xInicio + x * tamañoCasilla + tamañoCasilla / 2f,
                    yInicio + y * tamañoCasilla + tamañoCasilla / 2f,
                    0f
                );

                // Instanciamos la casilla
                var casilla = Instantiate(_casillaPrefab, posicion, Quaternion.identity);
                
                // La hacemos hija del objeto Tablero para tenerlo todo ordenado en el Hierarchy
                casilla.transform.parent = transform;

                // Obtenemos el SpriteRenderer para poder cambiar el color
                SpriteRenderer sr = casilla.GetComponent<SpriteRenderer>();

                // Ajustamos la escala de la casilla para que ocupe exactamente el tamaño calculado
                casilla.transform.localScale = new Vector3(tamañoCasilla, tamañoCasilla, 1f);

                _casillas[new Vector2(x, y)] = casilla;
            }
        }

        // Debido a la naturaleza del networking, el servidor debe esperar a que ambos clientes
        // estén conectados antes de continuar con las siguientes funciones, debido a que si no
        // están conectados, las funciones no se ejecutan en el lado del cliente
        StartCoroutine(nameof(EsperarJugadores));
    }

    // Espera a que se conecten ambos clientes para ejecutarse
    IEnumerator EsperarJugadores()
    {
        // El 'observers.Count' registra la cantidad de clientes conectados
        // Se necesita a dos, por lo que mientras sean menos, espera
        yield return new WaitUntil(() => observers.Count > 0);
        GameManager.instance.UpdateGameState(GameState.ColorTable);
    }

    [ServerRpc]
    public void cambiarColor()
    {
        Debug.Log("Init cambio color - isServer: " + isServer);

        if (!isServer) return;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                _casillas[new Vector2(x, y)].inicioCambioColor(x, y);
            }
        }
        GameManager.instance.UpdateGameState(GameState.SpawnWhites);
    }

    // No se por que, pero el cliente solo se actualiza de esta forma
    public void await()
    {
        GameManager.instance.UpdateGameState(GameState.WhiteTurn);
    }

    public Tile GetCaillaFromPosition(Vector2 pos)
    {
        if(_casillas.TryGetValue(pos, out var casilla))
        {
            return _casillas[pos];
        }

        return null;
    }

    public Dictionary<Vector2, Tile> GetCasillas()
    {
        return _casillas;
    }
}
