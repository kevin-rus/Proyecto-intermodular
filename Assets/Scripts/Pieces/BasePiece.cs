using UnityEngine;

// Clase base para las piezas
public abstract class BasePiece : MonoBehaviour
{
    public Casilla OccupiedCasilla;             // Casilla que ocupa
    public Player player;                       // Jugador al que pertenece
    public Vector2[][] movimientosPosibles;     // Array en el que se registran los posibles movimientos de la ficha en un turno

    public bool inCheck = false;

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

        Debug.Log("Detectando jaque para rey");
        casillaIni = casillaIni ?? OccupiedCasilla;
        Debug.Log(OccupiedCasilla);

        // ****** Detecta jaque en los ejes lineales (Torre, Reina) ******

        // Comprueba casilla en eje X positivo
        Debug.Log("Comprobando eje X positivo");
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX();
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX > 7 || posibMovY > 7) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                // CASILLA LIBRE
            }
            else if (posibCasilla.OccupiedPiece.player != player)
            {
                BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                if (enemyPiece is Rook || enemyPiece is Queen)
                {
                    Debug.Log("Rey en jaque por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    inCheck = true;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player) break;
        }

        // Comprueba casilla en eje X negativo
        Debug.Log("Comprobando eje X negativo");
        for (int i = -1; i >= -7; i--)
        {
            int posibMovX = casillaIni.getPosX();
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX < 0 || posibMovY < 0) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                // CASILLA LIBRE
            }
            else if (posibCasilla.OccupiedPiece.player != player)
            {
                BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                if (enemyPiece is Rook || enemyPiece is Queen)
                {
                    Debug.Log("Rey en jaque por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    inCheck = true;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player) break;
        }

        // Comprueba casilla en eje Y positivo
        Debug.Log("Comprobando eje Y positivo");
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + i;
            int posibMovY = casillaIni.getPosY();

            if (posibMovX > 7 || posibMovY > 7) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                // CASILLA LIBRE
            }
            else if (posibCasilla.OccupiedPiece.player != player)
            {
                BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                if (enemyPiece is Rook || enemyPiece is Queen)
                {
                    Debug.Log("Rey en jaque por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    inCheck = true;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player) break;
        }

        // Comprueba casilla en eje Y negativo
        Debug.Log("Comprobando eje Y negativo");
        for (int i = -1; i >= -7; i--)
        {
            int posibMovX = casillaIni.getPosX() + i;
            int posibMovY = casillaIni.getPosY();

            if (posibMovX < 0 || posibMovY < 0) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                // CASILLA LIBRE
            }
            else if (posibCasilla.OccupiedPiece.player != player)
            {
                BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                if (enemyPiece is Rook || enemyPiece is Queen)
                {
                    Debug.Log("Rey en jaque por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    inCheck = true;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player) break;
        }

        // ****** Detecta jaque en los ejes diagonales (Alfil, Reina) ******

        // Comprueba casilla en eje X positivo - eje Y positivo
        Debug.Log("Comprobando eje X positivo - eje Y positivo");
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + i;
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX > 7 || posibMovY > 7) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                // CASILLA LIBRE
            }
            else if (posibCasilla.OccupiedPiece.player != player)
            {
                BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                if (enemyPiece is Bishop || enemyPiece is Queen)
                {
                    Debug.Log("Rey en jaque por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    inCheck = true;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player) break;
        }

        // Comprueba casilla en eje X positivo - eje Y negativo
        Debug.Log("Comprobando eje X positvo - eje Y negativo");
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + i;
            int posibMovY = casillaIni.getPosY() + (i * -1);

            if (posibMovX > 7 || posibMovY < 0) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                // CASILLA LIBRE
            }
            else if (posibCasilla.OccupiedPiece.player != player)
            {
                BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                if (enemyPiece is Bishop || enemyPiece is Queen)
                {
                    Debug.Log("Rey en jaque por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    inCheck = true;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player) break;
        }

        // Comprueba casilla en eje X negativo - eje Y negativo
        Debug.Log("Comprobando eje X negativo - eje Y negativo");
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + (i * -1);
            int posibMovY = casillaIni.getPosY() + (i * -1);

            if (posibMovX < 0 || posibMovY < 0) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                // CASILLA LIBRE
            }
            else if (posibCasilla.OccupiedPiece.player != player)
            {
                BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                if (enemyPiece is Bishop || enemyPiece is Queen)
                {
                    Debug.Log("Rey en jaque por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    inCheck = true;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player) break;
        }

        // Comprueba casilla en eje X negativo - eje Y positivo
        Debug.Log("Comprobando eje X negativo - eje Y positivo");
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + (i * -1);
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX < 0 || posibMovY > 7) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                // CASILLA LIBRE
            }
            else if (posibCasilla.OccupiedPiece.player != player)
            {
                BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                if (enemyPiece is Bishop || enemyPiece is Queen)
                {
                    Debug.Log("Rey en jaque por pieza en casilla: " + posibMovX + " - " + posibMovY);
                    inCheck = true;
                }
                else break;
            }
            else if (posibCasilla.OccupiedPiece.player == player) break;
        }

        // ****** Detecta jaque por caballo ******

        // Comprueba casilla en eje X positivo
        Debug.Log("Comprobando eje X positivo");
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
                        Debug.Log("Rey en jaque por pieza en casilla: " + posibMovX + " - " + posibMovY);
                        inCheck = true;
                    }
                }
            }
        }

        // Comprueba casilla en eje X negativo
        Debug.Log("Comprobando eje X negativo");
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
                        Debug.Log("Rey en jaque por pieza en casilla: " + posibMovX + " - " + posibMovY);
                        inCheck = true;
                    }
                }
            }
        }

        // Comprueba casilla en eje Y positivo
        Debug.Log("Comprobando eje Y positivo");
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
                        Debug.Log("Rey en jaque por pieza en casilla: " + posibMovX + " - " + posibMovY);
                        inCheck = true;
                    }
                }
            }
        }

        // Comprueba casilla en eje Y negativo
        Debug.Log("Comprobando eje Y negativo");
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
                        Debug.Log("Rey en jaque por pieza en casilla: " + posibMovX + " - " + posibMovY);
                        inCheck = true;
                    }
                }
            }
        }

        // ****** Detecta jaque por peon ******

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
                    Debug.Log("Rey en jaque por pieza en casilla: " + leftCorner + " - " + posibMovYPawn);
                    inCheck = true;
                }
            }
            if (rightCorner <= 7)
            {
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(rightCorner, posibMovYPawn));
                if (posibCasilla.OccupiedPiece is Pawn && posibCasilla.OccupiedPiece.player != player)
                {
                    BasePiece enemyPiece = posibCasilla.OccupiedPiece;
                    Debug.Log("Rey en jaque por pieza en casilla: " + rightCorner + " - " + posibMovYPawn);
                    inCheck = true;
                }
            }
        }

        if (inCheck)
        {
            return true;
        }
        else return false;
    }
}