using System.Collections.Generic;
using UnityEngine;

// Clase base para las piezas
public abstract class BasePiece : MonoBehaviour
{
    public Casilla OccupiedCasilla;             // Casilla que ocupa
    public Player player;                       // Jugador al que pertenece
    public Vector2[][] movimientosPosibles;     // Array en el que se registran los posibles movimientos de la ficha en un turno
    public bool inCheck = false;

    public List<BasePiece> dangerPieces;        // Lista de piezas que ponen en jaque
    public List<Casilla> dangerPath;            // Lista de casillas que forman el camino de amenaza

    // Calcula los posibles movimientos de la ficha, dependiendo de su tipo y posición actual
    // El métedo está sujeto a cambios, según que clase de ficha se trate
    public abstract bool calcularMovimientos(Casilla casillaInic, Casilla casillaDese);

    public Casilla getCasilla()
    {
        return OccupiedCasilla;
    }

    public bool detectCheck(Casilla casillaIni)
    {
        bool inCheck = false;

        Debug.Log("Detectando jaque para " + this.name);
        casillaIni = casillaIni ?? OccupiedCasilla;
        Debug.Log(OccupiedCasilla);

        List<BasePiece> dangerPieces = new List<BasePiece>();
        List<Casilla> dangerPath = new List<Casilla>();

        // ****** Detecta jaque en los ejes lineales (Torre, Reina) ******
        Debug.Log("Comprobando ejes lineales");

        List<Casilla> pathPY = new List<Casilla>();
        // Comprueba casilla en eje Y positivo
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX();
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX > 7 || posibMovY > 7) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
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
                    Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    dangerPieces.Add(enemyPiece);

                    Debug.Log("Danger path: " + pathPY);
                    dangerPath = pathPY;

                    inCheck = true;
                    break;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player) break;
        }

        List<Casilla> pathNY = new List<Casilla>();
        // Comprueba casilla en eje Y negativo
        for (int i = -1; i >= -7; i--)
        {
            int posibMovX = casillaIni.getPosX();
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX < 0 || posibMovY < 0) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
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
                    Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    dangerPieces.Add(enemyPiece);

                    Debug.Log("Danger path: " + pathNY);
                    dangerPath = pathNY;

                    inCheck = true;
                    break;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player) break;
        }

        // Comprueba casilla en eje X positivo
        List<Casilla> pathPX = new List<Casilla>();
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + i;
            int posibMovY = casillaIni.getPosY();

            if (posibMovX > 7 || posibMovY > 7) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
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
                    Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    dangerPieces.Add(enemyPiece);

                    Debug.Log("Danger path: " + pathPX);
                    dangerPath = pathPX;

                    inCheck = true;
                    break;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player) break;
        }

        // Comprueba casilla en eje X negativo
        List<Casilla> pathNX = new List<Casilla>();
        for (int i = -1; i >= -7; i--)
        {
            int posibMovX = casillaIni.getPosX() + i;
            int posibMovY = casillaIni.getPosY();

            if (posibMovX < 0 || posibMovY < 0) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
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
                    Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    dangerPieces.Add(enemyPiece);

                    Debug.Log("Danger path: " + pathNX);
                    dangerPath = pathNX;

                    inCheck = true;
                    break;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player) break;
        }

        // ****** Detecta jaque en los ejes diagonales (Alfil, Reina) ******
        Debug.Log("Comprobando ejes diagonales");

        // Comprueba casilla en eje X positivo - eje Y positivo
        List<Casilla> pathPXPY = new List<Casilla>();
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + i;
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX > 7 || posibMovY > 7) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
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
                    Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    dangerPieces.Add(enemyPiece);

                    Debug.Log("Danger path: ");
                    foreach(Casilla c in pathPXPY) {
                        Debug.Log(c.name);
                    }
                    dangerPath = pathPXPY;

                    inCheck = true;
                    break;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player) break;
        }

        // Comprueba casilla en eje X positivo - eje Y negativo
        List<Casilla> pathPXNY = new List<Casilla>();
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + i;
            int posibMovY = casillaIni.getPosY() + (i * -1);

            if (posibMovX > 7 || posibMovY < 0) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
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
                    Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    dangerPieces.Add(enemyPiece);

                    Debug.Log("Danger path: " + pathPXNY);
                    dangerPath = pathPXNY;

                    inCheck = true;
                    break;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player) break;
        }

        // Comprueba casilla en eje X negativo - eje Y negativo
        List<Casilla> pathNXNY = new List<Casilla>();
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + (i * -1);
            int posibMovY = casillaIni.getPosY() + (i * -1);

            if (posibMovX < 0 || posibMovY < 0) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            Debug.Log(posibCasilla.name);
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
                    Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    dangerPieces.Add(enemyPiece);

                    Debug.Log("Danger path: NXNY");
                    dangerPath = pathNXNY;

                    inCheck = true;
                    break;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player) break;
        }

        // Comprueba casilla en eje X negativo - eje Y positivo
        List<Casilla> pathNXPY = new List<Casilla>();
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + (i * -1);
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX < 0 || posibMovY > 7) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
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
                    Debug.Log($"{name} en amenaza por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    dangerPieces.Add(enemyPiece);

                    Debug.Log("Danger path: " + pathNXPY);
                    dangerPath = pathNXPY;

                    inCheck = true;
                    break;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player) break;
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
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
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
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
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
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
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
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
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
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(leftCorner, posibMovYPawn));
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
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(rightCorner, posibMovYPawn));
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