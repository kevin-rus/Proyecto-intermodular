using System.Collections.Generic;
using UnityEngine;

public class Pawn : BasePiece
{
    private bool isFirstMove = true;

    public override bool calcularMovimientos(Casilla casillaIni, Casilla CasillaDese)
    {
        // Obtiene el rey del jugador para comprobar los movimientos cuando está en jaque
        King myKing = player == Player.White ? PieceManager.instance.GetWhiteKing() : PieceManager.instance.GetBlackKing();

        if (CasillaDese.OccupiedPiece != null && CasillaDese.OccupiedPiece.player != player)
        {
            int posibMovY = casillaIni.getPosY() + (player == Player.White ? 1 : -1);

            int leftCorner = casillaIni.getPosX() - 1;
            int rightCorner = casillaIni.getPosX() + 1;

            if (leftCorner >= 0)
            {
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(leftCorner, posibMovY));
                if (posibCasilla == CasillaDese)
                {
                    if(!myKing.inCheck)
                    {
                        Destroy(posibCasilla.OccupiedPiece.gameObject);
                        return true;
                    }
                    else
                    {
                        // Si el rey está en jaque, el peón solo puede comer la pieza que lo amenaza
                        if (myKing.dangerPieces.Count == 1 && myKing.dangerPieces.Contains(posibCasilla.OccupiedPiece))
                        {
                            Destroy(posibCasilla.OccupiedPiece.gameObject);
                            return true;
                        }
                    }
                }
            }
            if (rightCorner <= 7)
            {
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(rightCorner, posibMovY));
                if (posibCasilla == CasillaDese)
                {
                    if (!myKing.inCheck)
                    {
                        Destroy(posibCasilla.OccupiedPiece.gameObject);
                        return true;
                    }
                    else
                    {
                        // Si el rey está en jaque, el peón solo puede comer la pieza que lo amenaza
                        if (myKing.dangerPieces.Count == 1 && myKing.dangerPieces.Contains(posibCasilla.OccupiedPiece))
                        {
                            Destroy(posibCasilla.OccupiedPiece.gameObject);
                            return true;
                        }
                    }
                }
            }
        }

        // Registra los posibles movimientos en una lista, luego comprueba que el movimiento
        // deseado se encuentra en la lista
        List<Casilla> posibMovimientos = new List<Casilla>();

        // Durante el primer movimiento, el peón puede avanzar dos casillas
        for (int i = 1; i <= (isFirstMove ? 2 : 1); i++)
        {
            int posibMovX = casillaIni.getPosX();
            int posibMovY = casillaIni.getPosY() + (player == Player.White ? i : -i);

            // Si el movimiento se sale del tablero, se detiene el bucle
            if (posibMovX > 7 || posibMovY > 7) break;

            // Obtiene la casilla y comprueba que está libre para asignarla a la lista de movimientos posibles
            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                posibMovimientos.Add(posibCasilla);
            }
            else break;
        }

        // Si el movimiento es posible, devuelve true y asigna isFirstMove a false
        if (posibMovimientos.Contains(CasillaDese))
        {
            if(myKing.inCheck)
            {
                List<Casilla> dangerPath = myKing.dangerPath;
                // Si el rey está en jaque, el peón solo puede moverse a una casilla que bloquee el jaque
                if (myKing.dangerPieces.Count == 1 && dangerPath.Contains(CasillaDese))
                {
                    isFirstMove = false;
                    return true;
                }
            }
            else if (myKing.protectingPieces.Contains(this))
            {
                if(dangerPath.Contains(CasillaDese))
                {
                    Debug.Log("La pieza está protegiendo al rey y el movimiento está en el camino de peligro.");

                    isFirstMove = false;
                    return true;
                }
                else return false;
            }
            else
            {
                isFirstMove = false;
                return true;
            }
        }

        // Si no es posible, devuelve false
        return false;
    }
}
