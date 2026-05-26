using System.Collections.Generic;
using UnityEngine;

// Clase base para las piezas
public abstract class BasePiece : MonoBehaviour
{
    public Tile OccupiedCasilla;             // Casilla que ocupa
    public Player player;                       // Jugador al que pertenece
    public Vector2[][] movimientosPosibles;     // Array en el que se registran los posibles movimientos de la ficha en un turno
    public bool inCheck = false;

    public List<BasePiece> protectingPieces;    // Lista de piezas que protejen del jaque

    public List<BasePiece> dangerPieces;        // Lista de piezas que ponen en jaque
    public List<Tile> dangerPath;            // Lista de casillas que forman el camino de amenaza

    // Calcula los posibles movimientos de la ficha, dependiendo de su tipo y posición actual
    // El métedo está sujeto a cambios, según que clase de ficha se trate
    public abstract bool calcularMovimientos(Tile casillaInic, Tile casillaDese);

    public Tile getCasilla()
    {
        return OccupiedCasilla;
    }

    public bool detectCheck(Tile casillaIni)
    {
        bool inCheck = false;

        Debug.Log("Detectando jaque para " + this.name);
        casillaIni = casillaIni ?? OccupiedCasilla;
        Debug.Log(OccupiedCasilla);

        protectingPieces = new List<BasePiece>();               // Lista de piezas que protejen a la ficha
        bool dobleProtect = false;

        List<BasePiece> dangerPieces = new List<BasePiece>();   // Lista de piezas que ponen en jaque a la ficha
        List<Tile> dangerPath = new List<Tile>();         // Camino que forma la pieza atacante

        // ****** Detecta jaque en los ejes lineales (Torre, Reina) ******
        Debug.Log("Comprobando ejes lineales");

        BasePiece protectingPiece = null;
        List<Tile> pathPY = new List<Tile>();
        // Comprueba casilla en eje Y positivo
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX();
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX > 7 || posibMovY > 7) break;

            Tile posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                // CASILLA LIBRE
                pathPY.Add(posibCasilla);
            }
            else if (posibCasilla.OccupiedPiece.player != player)
            {
                BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                if (enemyPiece is Rook || enemyPiece is Queen)
                {
                    if(protectingPiece != null)
                    {
                        if(dobleProtect) break;
                        this.protectingPieces.Add(protectingPiece);
                        protectingPiece.dangerPath = pathPY;
                        break;
                    }

                    Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    dangerPieces.Add(enemyPiece);

                    dangerPath = pathPY;

                    inCheck = true;
                    break;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player)
            {
                if(protectingPiece == null) protectingPiece = posibCasilla.OccupiedPiece;
                else dobleProtect = true;
            }
        }

        protectingPiece = null;
        dobleProtect = false;
        List<Tile> pathNY = new List<Tile>();
        // Comprueba casilla en eje Y negativo
        for (int i = -1; i >= -7; i--)
        {
            int posibMovX = casillaIni.getPosX();
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX < 0 || posibMovY < 0) break;

            Tile posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                // CASILLA LIBRE
                pathNY.Add(posibCasilla);
            }
            else if (posibCasilla.OccupiedPiece.player != player)
            {
                BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                if (enemyPiece is Rook || enemyPiece is Queen)
                {
                    if (protectingPiece != null)
                    {
                        if(dobleProtect) break;
                        this.protectingPieces.Add(protectingPiece);
                        protectingPiece.dangerPath = pathNY;
                        break;
                    }
                    Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    dangerPieces.Add(enemyPiece);

                    dangerPath = pathNY;

                    inCheck = true;
                    break;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player)
            {
                if(protectingPiece == null) protectingPiece = posibCasilla.OccupiedPiece;
                else dobleProtect = true;
            }
        }

        // Comprueba casilla en eje X positivo
        protectingPiece = null;
        dobleProtect = false;
        List<Tile> pathPX = new List<Tile>();
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + i;
            int posibMovY = casillaIni.getPosY();

            if (posibMovX > 7 || posibMovY > 7) break;

            Tile posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                // CASILLA LIBRE
                pathPX.Add(posibCasilla);
            }
            else if (posibCasilla.OccupiedPiece.player != player)
            {
                BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                if (enemyPiece is Rook || enemyPiece is Queen)
                {
                    if (protectingPiece != null)
                    {
                        if(dobleProtect) break;
                        this.protectingPieces.Add(protectingPiece);
                        protectingPiece.dangerPath = pathPX;
                        break;
                    }

                    Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    dangerPieces.Add(enemyPiece);

                    dangerPath = pathPX;

                    inCheck = true;
                    break;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player)
            {
                if(protectingPiece == null) protectingPiece = posibCasilla.OccupiedPiece;
                else dobleProtect = true;
            }
        }

        // Comprueba casilla en eje X negativo
        protectingPiece = null;
        dobleProtect = false;
        List<Tile> pathNX = new List<Tile>();
        for (int i = -1; i >= -7; i--)
        {
            int posibMovX = casillaIni.getPosX() + i;
            int posibMovY = casillaIni.getPosY();

            if (posibMovX < 0 || posibMovY < 0) break;

            Tile posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                // CASILLA LIBRE
                pathNX.Add(posibCasilla);
            }
            else if (posibCasilla.OccupiedPiece.player != player)
            {
                BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                if (enemyPiece is Rook || enemyPiece is Queen)
                {
                    if (protectingPiece != null)
                    {
                        if(dobleProtect) break;
                        this.protectingPieces.Add(protectingPiece);
                        protectingPiece.dangerPath = pathNX;
                        break;
                    }

                    Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    dangerPieces.Add(enemyPiece);

                    dangerPath = pathNX;

                    inCheck = true;
                    break;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player)
            {
                if(protectingPiece == null) protectingPiece = posibCasilla.OccupiedPiece;
                else dobleProtect = true;
            }
        }

        // ****** Detecta jaque en los ejes diagonales (Alfil, Reina) ******
        Debug.Log("Comprobando ejes diagonales");

        // Comprueba casilla en eje X positivo - eje Y positivo
        protectingPiece = null;
        dobleProtect = false;
        List<Tile> pathPXPY = new List<Tile>();
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + i;
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX > 7 || posibMovY > 7) break;

            Tile posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                // CASILLA LIBRE
                pathPXPY.Add(posibCasilla);
            }
            else if (posibCasilla.OccupiedPiece.player != player)
            {
                BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                if (enemyPiece is Bishop || enemyPiece is Queen)
                {
                    if (protectingPiece != null)
                    {
                        if(dobleProtect) break;
                        this.protectingPieces.Add(protectingPiece);
                        protectingPiece.dangerPath = pathPXPY;
                        break;
                    }

                    Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    dangerPieces.Add(enemyPiece);

                    dangerPath = pathPXPY;

                    inCheck = true;
                    break;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player)
            {
                if(protectingPiece == null) protectingPiece = posibCasilla.OccupiedPiece;
                else dobleProtect = true;
            }
        }

        // Comprueba casilla en eje X positivo - eje Y negativo
        protectingPiece = null;
        dobleProtect = false;
        List<Tile> pathPXNY = new List<Tile>();
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + i;
            int posibMovY = casillaIni.getPosY() + (i * -1);

            if (posibMovX > 7 || posibMovY < 0) break;

            Tile posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                // CASILLA LIBRE
                pathPXNY.Add(posibCasilla);
            }
            else if (posibCasilla.OccupiedPiece.player != player)
            {
                BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                if (enemyPiece is Bishop || enemyPiece is Queen)
                {
                    if (protectingPiece != null)
                    {
                        if(dobleProtect) break;
                        this.protectingPieces.Add(protectingPiece);
                        protectingPiece.dangerPath = pathPXNY;
                        break;
                    }
                    Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    dangerPieces.Add(enemyPiece);

                    dangerPath = pathPXNY;

                    inCheck = true;
                    break;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player)
            {
                if(protectingPiece == null) protectingPiece = posibCasilla.OccupiedPiece;
                else dobleProtect = true;
            }
        }

        // Comprueba casilla en eje X negativo - eje Y negativo
        protectingPiece = null;
        dobleProtect = false;
        List<Tile> pathNXNY = new List<Tile>();
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + (i * -1);
            int posibMovY = casillaIni.getPosY() + (i * -1);

            if (posibMovX < 0 || posibMovY < 0) break;

            Tile posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                // CASILLA LIBRE
                pathNXNY.Add(posibCasilla);
            }
            else if (posibCasilla.OccupiedPiece.player != player)
            {
                BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                if (enemyPiece is Bishop || enemyPiece is Queen)
                {
                    if (protectingPiece != null)
                    {
                        if(dobleProtect) break;
                        this.protectingPieces.Add(protectingPiece);
                        protectingPiece.dangerPath = pathNXNY;
                        break;
                    }

                    Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    dangerPieces.Add(enemyPiece);

                    dangerPath = pathNXNY;

                    inCheck = true;
                    break;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player)
            {
                if(protectingPiece == null) protectingPiece = posibCasilla.OccupiedPiece;
                else dobleProtect = true;
            }
        }

        // Comprueba casilla en eje X negativo - eje Y positivo
        protectingPiece = null;
        dobleProtect = false;
        List<Tile> pathNXPY = new List<Tile>();
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + (i * -1);
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX < 0 || posibMovY > 7) break;

            Tile posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                // CASILLA LIBRE
                pathNXPY.Add(posibCasilla);
            }
            else if (posibCasilla.OccupiedPiece.player != player)
            {
                BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                if (enemyPiece is Bishop || enemyPiece is Queen)
                {
                    if (protectingPiece != null)
                    {
                        if(dobleProtect) break;
                        this.protectingPieces.Add(protectingPiece);
                        protectingPiece.dangerPath = pathNXPY;
                        break;
                    }

                    Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    dangerPieces.Add(enemyPiece);

                    dangerPath = pathNXPY;

                    inCheck = true;
                    break;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player)
            {
                if(protectingPiece == null) protectingPiece = posibCasilla.OccupiedPiece;
                else dobleProtect = true;
            }
        }

        // ****** Detecta jaque por caballo ******
        Debug.Log("Comprobando caballos");

        // Comprueba casilla en eje X positivo
        for (int i = 0; i < 2; i++)
        {
            int movL = i == 0 ? 1 : -1;

            int posibMovX = casillaIni.getPosX() + movL;
            int posibMovY = casillaIni.getPosY() + 2;

            if (!(posibMovX > 7 || posibMovY > 7 || posibMovX < 0 || posibMovY < 0))
            {
                Tile posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
                if (posibCasilla.OccupiedPiece == null)
                {
                    // CASILLA LIBRE
                }
                else if (posibCasilla.OccupiedPiece.player != player)
                {
                    BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                    if (enemyPiece is Horse)
                    {
                        Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                        dangerPieces.Add(enemyPiece);
                        inCheck = true;
                    }
                }
            }
        }

        // Comprueba casilla en eje X negativo
        for (int i = 0; i < 2; i++)
        {
            int movL = i == 0 ? 1 : -1;

            int posibMovX = casillaIni.getPosX() + movL;
            int posibMovY = casillaIni.getPosY() - 2;

            if (!(posibMovX > 7 || posibMovY > 7 || posibMovX < 0 || posibMovY < 0))
            {
                Tile posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
                if (posibCasilla.OccupiedPiece == null)
                {
                    // CASILLA LIBRE
                }
                else if (posibCasilla.OccupiedPiece.player != player)
                {
                    BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                    if (enemyPiece is Horse)
                    {
                        Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                        dangerPieces.Add(enemyPiece);
                        inCheck = true;
                    }
                }
            }
        }

        // Comprueba casilla en eje Y positivo
        for (int i = 0; i < 2; i++)
        {
            int movL = i == 0 ? 1 : -1;

            int posibMovX = casillaIni.getPosX() + 2;
            int posibMovY = casillaIni.getPosY() + movL;

            if (!(posibMovX > 7 || posibMovY > 7 || posibMovX < 0 || posibMovY < 0))
            {
                Tile posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
                if (posibCasilla.OccupiedPiece == null)
                {
                    // CASILLA LIBRE
                }
                else if (posibCasilla.OccupiedPiece.player != player)
                {
                    BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                    if (enemyPiece is Horse)
                    {
                        Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                        dangerPieces.Add(enemyPiece);
                        inCheck = true;
                    }
                }
            }
        }

        // Comprueba casilla en eje Y negativo
        for (int i = 0; i < 2; i++)
        {
            int movL = i == 0 ? 1 : -1;

            int posibMovX = casillaIni.getPosX() - 2;
            int posibMovY = casillaIni.getPosY() + movL;

            if (!(posibMovX > 7 || posibMovY > 7 || posibMovX < 0 || posibMovY < 0))
            {
                Tile posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
                if (posibCasilla.OccupiedPiece == null)
                {
                    // CASILLA LIBRE
                }
                else if (posibCasilla.OccupiedPiece.player != player)
                {
                    BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                    if (enemyPiece is Horse)
                    {
                        Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                        dangerPieces.Add(enemyPiece);
                        inCheck = true;
                    }
                }
            }
        }

        // ****** Detecta jaque por peon ******
        Debug.Log("Comprobando peones");

        int posibMovYPawn = casillaIni.getPosY() + (player == Player.White ? 1 : -1);

        int leftCorner = casillaIni.getPosX() - 1;
        int rightCorner = casillaIni.getPosX() + 1;

        if (posibMovYPawn < 7 && posibMovYPawn >= 0)
        {
            if (leftCorner >= 0)
            {
                Tile posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(leftCorner, posibMovYPawn));
                if (posibCasilla.OccupiedPiece is Pawn && posibCasilla.OccupiedPiece.player != player)
                {
                    BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                    Debug.Log($"{name} en amenaza por pieza en casilla: " + leftCorner + " - " + posibMovYPawn);
                    dangerPieces.Add(enemyPiece);
                    inCheck = true;
                }
            }
            if (rightCorner <= 7)
            {
                Tile posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(rightCorner, posibMovYPawn));
                if (posibCasilla.OccupiedPiece is Pawn && posibCasilla.OccupiedPiece.player != player)
                {
                    BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                    Debug.Log($"{name} en amenaza por pieza en casilla: " + rightCorner + " - " + posibMovYPawn);
                    dangerPieces.Add(enemyPiece);
                    inCheck = true;
                }
            }
        }

        if (inCheck)
        {
            if(casillaIni == OccupiedCasilla)
            {
                this.dangerPieces = dangerPieces;
                this.dangerPath = dangerPath;
            }

            return true;
        }
        else return false;
    }
}